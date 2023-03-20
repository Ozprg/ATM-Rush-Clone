using System;
using UnityEngine;

public class CollectibleMovementController : MonoBehaviour
{
    public bool isCollected { get; set; }
    public bool isStackedBefore { get; set; }
    public bool isLastStacked { get; set; }
    public Transform stackedTransform { get; set; }

    public float stackedObjectSpeed { get; set; }
    public float stackedObjectMaxXDifference  { get; set; }
    public float distanceBetweenStackedObjects  { get; set; }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (isCollected)
        {
            //stackedObjectSpeed hızıyla hareket edecek
            //stackedObjectMaxXDifference kadar x pozisyonu clamplenecek
            //distanceBetweenStackedObjects kadar z farkı olacak stackedTransform ile

            
            
            Vector3 thisPosition = transform.position;
            Vector3 stackedPosition = stackedTransform.position;

            thisPosition = new Vector3(
                Mathf.Clamp(Mathf.Lerp(thisPosition.x, stackedPosition.x, Mathf.Clamp(Time.deltaTime * stackedObjectSpeed, 0, 0.8f)),
                stackedPosition.x - stackedObjectMaxXDifference, 
                stackedPosition.x + stackedObjectMaxXDifference),
                        
                y:thisPosition.y,
                
                Mathf.Lerp(thisPosition.z, stackedPosition.z + distanceBetweenStackedObjects,
                    Mathf.Clamp(Time.deltaTime * stackedObjectSpeed, 0, 0.8f)));

            transform.position = thisPosition;
        }
    }

    public void FallFromSTheStack()
    {
        isCollected = false;
        stackedTransform = null;
        
        //Stackten düşme hareketi, zıplama, kaybolma, yok olma
    }

    public void EnableActivatedState()
    {
        if (!isStackedBefore)
        {
            if (!isCollected)
            {
                isStackedBefore = true;
                isCollected = true;
            }
        }
    }
}