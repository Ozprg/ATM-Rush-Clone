using DG.Tweening;
using System;
using UnityEngine;



public class CollectibleMovementController : MonoBehaviour
{
    public bool isCollected { get; set; }
    public bool isStackedBefore { get; set; }
    public bool isLastStacked { get; set; }
    public bool isOnAir { get; private set; }
    public Transform stackedTransform { get; set; }
    public CollectibleController collectibleController { get; set; }

    public float stackedObjectSpeed { get; set; }
    public float stackedObjectMaxXDifference  { get; set; }
    public float distanceBetweenStackedObjects  { get; set; }

    [SerializeField][Range(0,1)] float _soldMovementDuration =100;
    [SerializeField][Range(0, 1)] float _finidLineMovementDuration = 1f;

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
        isOnAir = true;
        
        Vector3 currentPosition = transform.position;
        Vector3 jumpPos= GetRandomJumpPosition(currentPosition);
        
        int aTMLayer = LayerMask.NameToLayer("ATM");
        int layerMask = 1 << aTMLayer;       

        while (Physics.OverlapSphere(jumpPos, 2, layerMask).Length > 0)
        {
            jumpPos = GetRandomJumpPosition(currentPosition);

        }

        transform.DOJump(jumpPos, 1, 1, 0.5f)
            .OnComplete(OnFallFromStackEnded);

        isCollected = false;
        stackedTransform = null;

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

    public void EnableCollusion()
    {
        isStackedBefore = false;
    }

    public void PerformATMSoldMovement(Transform finalMoneyDestination)
    {
        transform.DOMove(finalMoneyDestination.position, _soldMovementDuration).OnComplete(() 
            => gameObject.SetActive(false));
    }

    public void MoveTowardsAtmOnFinishLine(Transform finalAtmTransform, Transform finishLineTransform)
    {
        float yPosOffset = 1;
        isCollected = false;
        stackedTransform = null;

        Vector3 collectiblePositionAfterTouchedFinishLine = new Vector3
            (transform.position.x,
            finishLineTransform.position.y + yPosOffset,
            finishLineTransform.position.z);

        transform.position = collectiblePositionAfterTouchedFinishLine;
        transform.DOMoveX(-5.6f, _finidLineMovementDuration);

    }

    public void OnFallFromStackEnded()
    {
        isOnAir = false;
        EnableCollusion();
    }

    public Vector3 GetRandomJumpPosition(Vector3 currentPosition)
    {
        float randomXValue = UnityEngine.Random.Range(-3, 3);
        float randomZValue = currentPosition.z + UnityEngine.Random.Range(7, 13);
        return new Vector3(randomXValue, 0, randomZValue);
    }
}