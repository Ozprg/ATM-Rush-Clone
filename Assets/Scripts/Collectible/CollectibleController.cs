using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class CollectibleController : MonoBehaviour, IInteractable
{
    private int _currentLevel = 1; 
    private int _maxLevel = 0;
    private int _totalValueOfCollectedMoney;

    public bool _isSold;

    Transform _buyerAtmTransform;
    public int CurrentLevel => _currentLevel;

    public CollectibleMeshController meshController;    
    public CollectibleMovementController collectibleMovementController { get; private set; }
    public CollectibleCollisionController collectibleCollisionController { get; private set; }

    private void OnEnable()
    {

        LevelController.Instance.OnPlayerTouchedGate += UpgradeMoney;
        LevelController.Instance.OnCollectibleTouchedATM += SellCollectible;
    }

    private void OnDisable()
    {
        
        LevelController.Instance.OnPlayerTouchedGate -= UpgradeMoney;
        LevelController.Instance.OnCollectibleTouchedATM -= SellCollectible;
    }



    private void Awake()
    {
        collectibleMovementController = GetComponent<CollectibleMovementController>();
        collectibleCollisionController = GetComponent<CollectibleCollisionController>();
        meshController = GetComponent<CollectibleMeshController>();

        if (collectibleCollisionController)
        {
            collectibleCollisionController.collectibleController = this;
        }

        if (collectibleMovementController)
        {
            collectibleMovementController.collectibleController = this;
        }
    }

    private void Start()
    {
        _maxLevel = meshController.meshes.Length;
        _totalValueOfCollectedMoney = 0;
    }

 

    public void Interact()
    {
        if (collectibleMovementController)
        {
            if (!collectibleMovementController.isStackedBefore)
            {
                collectibleMovementController.EnableActivatedState();
                
            }
        }
    }

    public void UpgradeMoney(CollectibleController collectibleController)
    {
        if (collectibleController == this)
        {
            _currentLevel += 1;

            if (_currentLevel > _maxLevel)
            {
                _currentLevel = _maxLevel;
            }

            meshController.UpgradeBody(_currentLevel);

        }

    }

    private void SellCollectible(CollectibleController collectibleController, ATMController aTMController)
    {
        if (collectibleController == this)
        {
            _isSold = true;
            _buyerAtmTransform = aTMController.cashLocation;
            LevelController.Instance.CollectibleSold(collectibleController);
        }
    }

    public void LoseAsStackedObject()
    {
        collectibleMovementController.FallFromSTheStack();
    }

    public void LoseAsSoldObject()
    {
        collectibleMovementController.isCollected = false;
        collectibleMovementController.PerformATMSoldMovement(_buyerAtmTransform);
    }

    


}