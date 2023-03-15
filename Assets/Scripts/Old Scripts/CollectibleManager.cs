using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class CollectibleManager : MonoBehaviour, ICollectible
{
    [SerializeField] private List<GameObject> collectibleList = new List<GameObject>();
    [SerializeField] private float distanceBetweenStackedCollectibles = .5f;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.transform.SetParent(transform);
            AddToStack(other.gameObject);

            if (collectibleList.Count == 1)
            {
                other.gameObject.transform.localPosition = new Vector3(0f, 0f, distanceBetweenStackedCollectibles);

            }
            else if (collectibleList.Count > 1)
            {
                other.gameObject.transform.localPosition = new Vector3(0f, 0f, collectibleList[collectibleList.Count - 1].transform.localPosition.z + distanceBetweenStackedCollectibles);

            }
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (collectibleList.Count >= 1)
            {                
                collectibleList[collectibleList.Count - 1].transform.parent = null;
                collectibleList[collectibleList.Count - 1].transform.DOJump(Vector3.forward, 3, 1, 1);
                RemoveFromStack(collectibleList[collectibleList.Count - 1]);

            }
        }
    }
    public void AddToStack(GameObject stackableCollectible)
    {
        collectibleList.Add(stackableCollectible);
        StartCoroutine(CollectedFeedback());
    }

    public void RemoveFromStack(GameObject stackedCollectibleList)
    {
        collectibleList.Remove(stackedCollectibleList);
    }

    private IEnumerator CollectedFeedback()
    {
        for (int index = collectibleList.Count - 1; index >= 0; index--)
        {
            if (index >= 0 && index < collectibleList.Count)
            {
                collectibleList[index].transform.GetChild(0).DOPunchScale(Vector3.one * .5f, 1);
                yield return new WaitForSeconds(0.05f);
            }
        }       
    }

}
