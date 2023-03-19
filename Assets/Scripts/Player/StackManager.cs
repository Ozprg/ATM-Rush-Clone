using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private float stackedObjectSpeed;
    [SerializeField] [Range(0, 50)] private float stackedObjectMaxXDifference;
    [SerializeField] [Range(0, 5)] private float distanceBetweenStackedObjects;
    [SerializeField] [Range(0, 2)] private float stackedFeedbackScaleRate;
    [SerializeField] [Range(0, 1)] private float stackedFeedbackDuration;
    [SerializeField] private Transform frontStackTransform;
    
    private List<CollectibleController> _stackedObjectList;
    private Transform _lastStackedTransform;
    private int _stackedIndex;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _stackedObjectList = new List<CollectibleController>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        _lastStackedTransform = frontStackTransform;
    }

    private void OnEnable()
    {
        LevelController.Instance.OnPlayerCollectedCollectible += OnPlayerCollectedCollectible;
        LevelController.Instance.OnStackedObjectHitObstacle += OnStackedObjectHitObstacle;
    }

    private void OnDisable()
    {
        LevelController.Instance.OnPlayerCollectedCollectible -= OnPlayerCollectedCollectible;
        LevelController.Instance.OnStackedObjectHitObstacle -= OnStackedObjectHitObstacle;
    }
    
    private void OnPlayerCollectedCollectible(CollectibleController stackingController)
    {
        if (stackingController)
        {
            stackingController.collectibleMovementController.stackedObjectSpeed = this.stackedObjectSpeed;
            stackingController.collectibleMovementController.stackedObjectMaxXDifference = this.stackedObjectMaxXDifference;
            stackingController.collectibleMovementController.distanceBetweenStackedObjects = this.distanceBetweenStackedObjects;
            
            _stackedIndex++;
            _stackedObjectList.Add(stackingController);

            CollectibleMovementController lastController =
                _lastStackedTransform.GetComponent<CollectibleMovementController>();

            CollectibleMovementController currentController = stackingController.collectibleMovementController;
            
            if(lastController) lastController.isLastStacked = false;
                
            currentController.stackedTransform = _lastStackedTransform;
            _lastStackedTransform = stackingController.transform;
            currentController.isLastStacked = true;
            
            //max angle atama

            //StartCoroutine(DoCollectedFeedback());
        }
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

    private void LoseObject(CollectibleController falledObject)
    {
        int index = _stackedObjectList.IndexOf(falledObject);

        _stackedObjectList.RemoveAt(index);
        _lastStackedTransform = falledObject.collectibleMovementController.stackedTransform;
        _stackedIndex--;
        _boxCollider.isTrigger = _stackedIndex > 1;
        falledObject.LoseAsStackedObject();
    }

    private IEnumerator DoCollectedFeedback()
    {
        Debug.Log("123123123123");
        for (int index = _stackedObjectList.Count -1; index >= 0; index--)
        {
            _stackedObjectList[index].body.DOPunchScale(Vector3.one * stackedFeedbackScaleRate, stackedFeedbackDuration, 5, 0.5f);
            yield return new WaitForSeconds(0.05f);
        }       
    }
}