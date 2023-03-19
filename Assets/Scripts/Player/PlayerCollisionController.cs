using Unity.VisualScripting;
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
                    LevelController.Instance.PlayerCollectedCollectible(collectedCollectible);
                }
            }
            
            IInteractable interactable = collidedObject.GetComponent<IInteractable>();
            interactable?.Interact();
        }
    }
}