using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ATMController : MonoBehaviour
{
    [SerializeField] private TextMeshPro _moneyText;
    public Transform cashLocation;

    private void OnEnable()
    {
        LevelController.Instance.OnCollectibleTouchedATM += CountMoney;
    }

    private void OnDisable()
    {
        LevelController.Instance.OnCollectibleTouchedATM -= CountMoney;
    }

    private void CountMoney(CollectibleController  collectibleController, ATMController aTMController)
    {
        MoneyManager.Instance.totalValueOfMoney += collectibleController.CurrentLevel * 1;
        SetMoneyText(MoneyManager.Instance.totalValueOfMoney);       
    }

    private void SetMoneyText(int _money)
    {
        _moneyText.text = _money.ToString();
    }
}
