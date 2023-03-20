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
                        Debug.Log(("player değdi"));

                        LevelController.Instance.PlayerCollectedCollectible(collectedCollectible);
                    }
                }
            }
            
            IInteractable interactable = collidedObject.GetComponent<IInteractable>();
            interactable?.Interact();
        }
    }
}