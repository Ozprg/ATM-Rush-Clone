using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Collectibles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="UnstackedCollectible" && !CollectibleManager1.Instance.collectibleList.Contains(other.gameObject))
        {
            other.gameObject.tag = "StackedCollectible";
            other.GetComponent<SphereCollider>().isTrigger = true;                  
            other.gameObject.AddComponent<Collectibles>();    
            other.gameObject.AddComponent<Rigidbody>();
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            CollectibleManager1.Instance.AddToStack(other.gameObject, CollectibleManager1.Instance.collectibleList.Count -1);
        }

        if (other.gameObject.tag == "Obstacle")
        {
            
            int indexOfColludedObject = CollectibleManager1.Instance.collectibleList.IndexOf(gameObject);           
            CollectibleManager1.Instance.RemoveFromStack(gameObject, indexOfColludedObject);
        }
    }


}
