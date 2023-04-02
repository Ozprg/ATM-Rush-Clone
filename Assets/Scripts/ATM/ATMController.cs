using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ATMController : MonoBehaviour
{
    private int _totalValueOfMoney;

    public Transform cashLocation;

    [SerializeField] private TextMeshPro _moneyText;    

    private void Awake()
    {
        _totalValueOfMoney = 0;
    }
    private void OnEnable()
    {
        LevelController.Instance.OnCollectibleTouchedATM += CountMoney
;
    }

    private void OnDisable()
    {
        LevelController.Instance.OnCollectibleTouchedATM -= CountMoney;
    }

    
    public void CountMoney(CollectibleController  collectibleController, ATMController aTMController)
    {

        _totalValueOfMoney += collectibleController.CurrentLevel * 1;
        SetMoneyText(_totalValueOfMoney);
        
    }

    public void SetMoneyText(int _money)
    {
        _moneyText.text = _money.ToString();
    }



}
