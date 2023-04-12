using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;

public class FinishLineController : MonoBehaviour
{
    [SerializeField] private Transform finalATMTransform;
    [SerializeField] private Transform finishLineTransform;

    
    private void OnEnable()
    {
        LevelController.Instance.OnStackedObjectCollidedWithFinishLine += OnLevelCompleted;
    }
    private void OnDisable()
    {
        LevelController.Instance.OnStackedObjectCollidedWithFinishLine -= OnLevelCompleted;
    }

    public void OnLevelCompleted(CollectibleController collectibleController)
    {
        collectibleController.collectibleMovementController.MoveTowardsAtmOnFinishLine(finalATMTransform, finishLineTransform);
    } 

}
