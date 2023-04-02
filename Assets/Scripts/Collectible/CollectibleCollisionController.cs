using UnityEngine;

public class CollectibleCollisionController : CollisionController
{
    public CollectibleController collectibleController { get; set; }

    protected override void DetectCollision(Collision other)
    {
        GameObject collidedObject = other.gameObject;

        if (collidedObject.CompareTag(interactableTag))
        {
            if (collectibleController.collectibleMovementController.isCollected)
            {
                if (collidedObject.layer == obstacleLayer)
                {                  
                    LevelController.Instance.StackedObjectHitObstacle(collectibleController);
                }

                if(collidedObject.layer == gateLayer)
                {
                    LevelController.Instance.PlayerTouchedGate(collectibleController);
                }
               
                if (collidedObject.layer == ATMlayer)
                {
                    if (!collectibleController.collectibleMovementController.isOnAir)
                    {
                        LevelController.Instance.CollectibleTouchedATM(collectibleController, other.transform.GetComponent<ATMController>());
                    }                    
                }

                if (collidedObject.layer == collectibleLayer)
                {
                    CollectibleController collectedCollectible = collidedObject.GetComponent<CollectibleController>();
                    if (collectedCollectible)
                    {
                        if (!collectedCollectible.collectibleMovementController.isStackedBefore)
                        {
                            LevelController.Instance.PlayerCollectedCollectible(collectedCollectible);
                        }
                    }
                }
                
                IInteractable interactable = collidedObject.GetComponent<IInteractable>();
                interactable?.Interact();
            }

            
        }
    }
}