using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCollectiblesAfterFinishLine : MonoBehaviour
{
    [SerializeField] GameObject _finishLineCollectibles;
    
    private void OnEnable()
    {
        LevelController.Instance.OnPlayerCollidedWithFinishLine += CreateCollectiblesWhenLevelIsFinished;
    }

    private void OnDisable()
    {
        LevelController.Instance.OnPlayerCollidedWithFinishLine -= CreateCollectiblesWhenLevelIsFinished;
    }

    public void CreateCollectiblesWhenLevelIsFinished()
    {
        for (int i = 1; i <= MoneyManager.Instance.totalValueOfMoney; i++)
        {
            Vector3 collectiblePos = new Vector3
                (transform.position.x, 
                transform.position.y - i,
                transform.position.z);

            GameObject collectibles = Instantiate(_finishLineCollectibles, collectiblePos, Quaternion.identity);
            collectibles.transform.SetParent(transform);

            Debug.Log("Collectible üretildi");
        }

        Debug.Log(MoneyManager.Instance.totalValueOfMoney + " Kadar collectible üretildi");   
    }
}

