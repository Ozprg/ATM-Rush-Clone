using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ATMController : MonoBehaviour
{
    private int _totalValueOfMoney;
    [SerializeField] private TextMeshPro _moneyText;
    public Transform cashLocation;

    ATMCollisionController aTMCollisionController;

    private void Awake()
    {
        aTMCollisionController = GetComponent< ATMCollisionController >();
        aTMCollisionController.aTMController= this;

        _totalValueOfMoney = 0;
    }
    private void OnEnable()
    {
        LevelController.Instance.OnPlayerTouchedATM += CountMoney
;
    }

    private void OnDisable()
    {
        LevelController.Instance.OnPlayerTouchedATM -= CountMoney;
    }

    
    public void CountMoney(CollectibleController  collectibleController, ATMController aTMController)
    {

        _totalValueOfMoney += collectibleController.CurrentLevel * 1;

        Debug.Log(collectibleController.CurrentLevel);
        Debug.Log(_totalValueOfMoney);

        SetMoneyText(_totalValueOfMoney);
        //collectibleController.gameObject.SetActive(false);
    }

    public void SetMoneyText(int _money)
    {
        _moneyText.text = _money.ToString();
    }



}
