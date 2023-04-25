using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class MoneyMultiplierMover : MonoBehaviour
{
    [SerializeField] private float durationOfForwardMovement;
    private Transform transformToMoveForward;
    private int indexOfMultiplier;   
    private int maxAmountOfMultiplierBlocks = 10;

    private void OnEnable()
    {
        LevelController.Instance.OnPlayerFinishedUpwardsMovement += OnPlayerFinishedUpwardsMovement;
    }
    private void OnDisable()
    {
        LevelController.Instance.OnPlayerFinishedUpwardsMovement -= OnPlayerFinishedUpwardsMovement;
    }

    private void OnPlayerFinishedUpwardsMovement()
    {
        if (ATMController.Instance.TotalMoney <= maxAmountOfMultiplierBlocks)
        {
            indexOfMultiplier = ATMController.Instance.TotalMoney;
            transformToMoveForward = FinishLineMoneyMultiplierManager.Instance.moneyMultiplierCubeList[indexOfMultiplier];
            transformToMoveForward.transform.DOMoveZ(transformToMoveForward.position.z - 3, durationOfForwardMovement);
        }
        else if (ATMController.Instance.TotalMoney > maxAmountOfMultiplierBlocks)
        {
            indexOfMultiplier = FinishLineMoneyMultiplierManager.Instance.moneyMultiplierCubeList.Count - 1;
            transformToMoveForward = FinishLineMoneyMultiplierManager.Instance.moneyMultiplierCubeList[indexOfMultiplier];
            transformToMoveForward.transform.DOMoveZ(transformToMoveForward.position.z - 3, durationOfForwardMovement);
        }
    }
}
