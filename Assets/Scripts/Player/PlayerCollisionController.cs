using UnityEngine;

public class PlayerCollisionController : CollisionController
{
    protected override void DetectCollision(Collision other)
    {
        GameObject collidedObject = other.gameObject;

        if (collidedObject.CompareTag(interactableTag))
        {
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

            if (collidedObject.layer == obstacleLayer)
            {
                LevelController.Instance.PlayerCollidedWithObstacle(transform);
            }
            
            IInteractable interactable = collidedObject.GetComponent<IInteractable>();
            interactable?.Interact();
        }
    }
}