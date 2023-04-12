using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private float stackedObjectSpeed;
    [SerializeField] [Range(0, 50)] private float stackedObjectMaxXDifference;
    [SerializeField] [Range(0, 5)] private float distanceBetweenStackedObjects;
    [SerializeField] [Range(0, 2)] private float stackedFeedbackScaleRate;
    [SerializeField] [Range(0, 1)] private float stackedFeedbackDuration;
    [SerializeField] [Range(0, 1)] private float stackedFeedbackDelay;
    [SerializeField] private Transform frontStackTransform;
    private bool _isFirstStackCollided;
    private Transform _lastStackedTransform;
    private List<CollectibleController> _stackedObjectList;
    public List<CollectibleController> StackedObjectList => _stackedObjectList;
    public int _finishStackCount;
    
    private void Awake()
    {
        _stackedObjectList = new List<CollectibleController>();
    }

    private void Start()
    {
        _lastStackedTransform = frontStackTransform;
    }

    private void OnEnable()
    {
        LevelController.Instance.OnPlayerCollectedCollectible += OnPlayerCollectedCollectible;
        LevelController.Instance.OnStackedObjectHitObstacle += OnStackedObjectHitObstacle;
        LevelController.Instance.OnCollectibleSold += OnCollectibleSold;
        LevelController.Instance.OnStackedObjectCollidedWithFinishLine += OnStackedObjectCollidedWithFinishLine;
    }

    private void OnDisable()
    {
        LevelController.Instance.OnPlayerCollectedCollectible -= OnPlayerCollectedCollectible;
        LevelController.Instance.OnStackedObjectHitObstacle -= OnStackedObjectHitObstacle;
        LevelController.Instance.OnCollectibleSold -= OnCollectibleSold;
        LevelController.Instance.OnStackedObjectCollidedWithFinishLine -= OnStackedObjectCollidedWithFinishLine;
    }



    private void OnPlayerCollectedCollectible(CollectibleController stackingController)
    {
        stackingController.collectibleMovementController.stackedObjectSpeed = this.stackedObjectSpeed;
        stackingController.collectibleMovementController.stackedObjectMaxXDifference = this.stackedObjectMaxXDifference;
        stackingController.collectibleMovementController.distanceBetweenStackedObjects = this.distanceBetweenStackedObjects;
        
        _stackedObjectList.Add(stackingController);
       
        CollectibleMovementController lastController =
            _lastStackedTransform.GetComponent<CollectibleMovementController>();
        CollectibleMovementController currentController = stackingController.collectibleMovementController;
      
        if (lastController) lastController.isLastStacked = false;
        currentController.stackedTransform = _lastStackedTransform;
        _lastStackedTransform = stackingController.transform;
        currentController.isLastStacked = true;

        StartCoroutine(DoCollectedFeedback());
    }

    private void OnStackedObjectHitObstacle(CollectibleController stackedObject)
    {
        if (_stackedObjectList.Contains(stackedObject))
        {
            OnObjectFalledAndFallAllAbove(stackedObject);
        }
    }

    private void OnObjectFalledAndFallAllAbove(CollectibleController stackedObject)
    {
        int index = _stackedObjectList.IndexOf(stackedObject);
        for (int i = _stackedObjectList.Count - 1; i >= index; i--)
        {
            LoseObject(_stackedObjectList[i]);
        }
    }

    private void LoseObject(CollectibleController falledObject, bool isFinishLine = false)
    {
        int index = _stackedObjectList.IndexOf(falledObject);
        _stackedObjectList.RemoveAt(index);
        _lastStackedTransform = falledObject.collectibleMovementController.stackedTransform;
        if (!isFinishLine)
        {
            if (falledObject._isSold == true)
            {
                falledObject.LoseAsSoldObject();
            }
            else
            {
                falledObject.LoseAsStackedObject();
            }
        }
        
    }

    private IEnumerator DoCollectedFeedback()
    {
        for (int i = _stackedObjectList.Count - 1; i >= 0; i--)
        {
            if (i >= 0 && i< _stackedObjectList.Count)
            {
                DOTween.Kill(_stackedObjectList[i].meshController.body);
                _stackedObjectList[i].meshController.body.localScale = Vector3.one;
                _stackedObjectList[i].meshController.body
                    .DOPunchScale(Vector3.one * stackedFeedbackScaleRate, stackedFeedbackDuration, 0, 0);
                yield return new WaitForSeconds(stackedFeedbackDelay);
            }
            
        }
    }

    private void OnCollectibleSold(CollectibleController stackedObject)
    {
        LoseObject(stackedObject);
    }

    private void OnStackedObjectCollidedWithFinishLine(CollectibleController collectibleController)
    {
        if (!_isFirstStackCollided)
        {
            _isFirstStackCollided = true;
            _finishStackCount = _stackedObjectList.Count;
        }

        LoseObject(collectibleController, true);

    }

}