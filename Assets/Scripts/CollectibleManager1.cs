using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using System;

public class CollectibleManager1 : MonoBehaviour
{
    public static CollectibleManager1 Instance { get; private set; }

    [SerializeField] public List<GameObject> collectibleList = new List<GameObject>();
    [SerializeField] private float distanceBetweenStackedCollectibles = .7f;

    [SerializeField] private Transform collectiblesParent;
    [SerializeField] private Transform disableCollectibleParent;

    [SerializeField] private float movementDelay = 0.5f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            MoveStackedElements();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            MoveStackedElementsToOrigin();
        }
    }

    public void AddToStack(GameObject stackableCollectible, int index)
    {
        collectibleList.Add(stackableCollectible);
        stackableCollectible.transform.parent = collectiblesParent;
        Vector3 positionAfterStacked = collectibleList[index].transform.localPosition;
        positionAfterStacked.y = stackableCollectible.transform.localPosition.y;
        positionAfterStacked.z += distanceBetweenStackedCollectibles;
        stackableCollectible.transform.localPosition = positionAfterStacked;
        

        StartCoroutine(DoTweenWhenCollected());
    }

    public void RemoveFromStack(GameObject colludedGameObject, int indexOfColludedObject)
    {
        collectibleList.RemoveAt(indexOfColludedObject);

        for (int i = indexOfColludedObject; i < collectibleList.Count ; i++)
        {
                colludedGameObject.transform.parent = disableCollectibleParent;
                //collectibleList.RemoveRange(i, collectibleList.Count - indexOfColludedObject);
        }
    }

    private IEnumerator DoTweenWhenCollected()
    {
        for (int i = collectibleList.Count -1; i >= 0; i--)
        {
            int index = i;
            collectibleList[index].transform.DOScale(Vector3.one * 1.5f, 0.1f).OnComplete(()
            => collectibleList[index].transform.DOScale(Vector3.one, 0.1f));
            yield return new WaitForSeconds(0.05f);

        }       
    }

    public void MoveStackedElements()
    {
        for (int i = 1; i < collectibleList.Count; i++)
        {
            Vector3 positionOfEachSphere = collectibleList[i].transform.localPosition;
            positionOfEachSphere.x = collectibleList[i - 1].transform.localPosition.x;
            collectibleList[i].transform.DOLocalMove(positionOfEachSphere, movementDelay);

        }
    }
    public void MoveStackedElementsToOrigin()
    {
        for (int i = 1; i < collectibleList.Count; i++)
        {
            Vector3 positionOfEachSphere = collectibleList[i].transform.localPosition;
            positionOfEachSphere.x = collectibleList[0].transform.localPosition.x;
            collectibleList[i].transform.DOLocalMove(positionOfEachSphere, .5f);
        }
    }

}
