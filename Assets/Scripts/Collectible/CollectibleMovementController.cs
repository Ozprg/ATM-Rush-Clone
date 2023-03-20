using DG.Tweening;
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

        Vector3 thisPosition = transform.position;
        float randomXPos = Random.Range(-2, 2);
        float randomZPos = thisPosition.z + Random.Range(7, 12);

        Vector3 jumpPosition = new Vector3(randomXPos, 0, randomZPos);

        transform
            .DOJump(jumpPosition, 1, 1, 0.5f)
            .OnComplete(EnableCollision);
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

    private void EnableCollision()
    {
        isStackedBefore = false;
    }
}