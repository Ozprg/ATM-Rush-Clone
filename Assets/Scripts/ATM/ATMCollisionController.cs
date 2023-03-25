using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATMCollisionController : CollisionController
{
    public ATMController aTMController;
    protected override void DetectCollision(Collision other)
    {
        GameObject collidedObject = other.gameObject;

        if (collidedObject.CompareTag(interactableTag))
        {
            CollectibleController collectedCollectible = collidedObject.GetComponent<CollectibleController>();
            
            if (collidedObject.layer == collectibleLayer)
            {
                LevelController.Instance.PlayerTouchedATM(collectedCollectible, aTMController);
            }
        }
    }
} 
