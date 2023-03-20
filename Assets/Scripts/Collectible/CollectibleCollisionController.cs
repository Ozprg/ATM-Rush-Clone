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
                    LevelController.Instance.PlayerCollidedWithObstacle(transform);
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