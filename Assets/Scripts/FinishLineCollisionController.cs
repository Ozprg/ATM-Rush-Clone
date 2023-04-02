using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineCollisionController : CollisionController
{
    protected override void DetectCollision(Collision other)
    {
        CollectibleController collectibleController;

        GameObject collidedObject = other.gameObject;
        
        if (collidedObject.CompareTag(interactableTag))
        {
            collectibleController = other.gameObject.GetComponent<CollectibleController>();

            if (collidedObject.layer == collectibleLayer)
            {
                LevelController.Instance.StackedObjectCollidedWithFinishLine(collectibleController);
            }

        }

        if (collidedObject.layer == PlayerLayer)
        {
            LevelController.Instance.PlayerCollidedWithFinishLine();
        }


    }
}
