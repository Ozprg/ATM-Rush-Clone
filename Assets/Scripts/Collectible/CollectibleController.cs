using System;
using UnityEngine;

public class CollectibleController : MonoBehaviour, IInteractable
{
    private int _currentLevel = 1;
    private int _maxLevel = 0;

    public CollectibleMeshController meshController;    
    public CollectibleMovementController collectibleMovementController { get; private set; }
    public CollectibleCollisionController collectibleCollisionController { get; private set; }

    private void OnEnable()
    {
        LevelController.Instance.OnPlayerTouchedGate += UpgradeMoney;

    }

    private void OnDisable()
    {
        LevelController.Instance.OnPlayerTouchedGate -= UpgradeMoney;
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
    }

    private void Start()
    {
        _maxLevel = meshController.meshes.Length;
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

    public void LoseAsStackedObject()
    {
        collectibleMovementController.FallFromSTheStack();
    }

    public void UpgradeMoney(CollectibleController collectibleController)
    {
        if (collectibleController ==this)
        {
            _currentLevel += 1;
            
            if (_currentLevel <= _maxLevel)
            {
                _currentLevel = _maxLevel;
            }

            meshController.UpgradeBody(_currentLevel);
        }

        
    }
}