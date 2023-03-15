using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class CollectibleMovement : MonoBehaviour
{
    public bool isCollected;    
    public bool isStackedBefore;
    public float zDistance;
    public float maxAngleDifference;
    public Transform stackedTransform;
    private float _speed;
    private float _stackedObjectSpeed;
    private float _stackedObjectMaxXDifference;


    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (isCollected)
        {
            //stackedTransformu takip et
        }
    }

    public void FallFromStack()
    {
        isCollected = false;
        stackedTransform = null;
        //objeyi zıplatırsın
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="UnstackedCollectible" && !CollectibleManager1.Instance.collectibleList.Contains(other.gameObject))
        {
            other.gameObject.tag = "StackedCollectible";
            other.GetComponent<SphereCollider>().isTrigger = true;                  
            other.gameObject.AddComponent<CollectibleMovement>();    
            other.gameObject.AddComponent<Rigidbody>();
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            CollectibleManager1.Instance.AddToStack(other.gameObject);
        }

        if (other.gameObject.tag == "Obstacle")
        {
            
            int indexOfColludedObject = CollectibleManager1.Instance.collectibleList.IndexOf(gameObject);           
            CollectibleManager1.Instance.OnObjectFalledAndFalLAllAbove(gameObject);
        }
    }


}
