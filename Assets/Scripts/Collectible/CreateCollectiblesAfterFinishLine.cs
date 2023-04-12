using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCollectiblesAfterFinishLine : MonoBehaviour
{
    [SerializeField] Transform _playertransform;
    [SerializeField] GameObject _finishLineCollectibles;
    [SerializeField] int numberOfCollectibles;
    
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
        for (int i = 1; i < numberOfCollectibles; i++)
        {
            Vector3 collectiblePos = new Vector3
                (_playertransform.position.x, 
                _playertransform.position.y - i,
                _playertransform.position.z);

            GameObject collectibles = Instantiate(_finishLineCollectibles, collectiblePos, Quaternion.identity);
            collectibles.transform.parent = _playertransform;

        }
       
    }


}

