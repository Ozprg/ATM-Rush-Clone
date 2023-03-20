using System;
using UnityEngine;

public class CollectibleController : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _body;
    public Transform body => _body;
    public CollectibleMovementController collectibleMovementController { get; private set; }
    public CollectibleCollisionController collectibleCollisionController { get; private set; }
    
    private void Awake()
    {
        collectibleMovementController = GetComponent<CollectibleMovementController>();
        collectibleCollisionController = GetComponent<CollectibleCollisionController>();

        if (collectibleCollisionController)
        {
            collectibleCollisionController.collectibleController = this;
        }
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
}