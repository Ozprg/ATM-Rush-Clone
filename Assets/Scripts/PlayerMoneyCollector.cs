using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneyCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UnstackedCollectible"))
        {

            other.gameObject.tag = "StackedCollectible";
            other.GetComponent<SphereCollider>().isTrigger = true;
            other.gameObject.AddComponent<Rigidbody>();
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.AddComponent<CollectibleMovement>();
            CollectibleManager1.Instance.AddToStack(other.gameObject);
        }
    }
}
