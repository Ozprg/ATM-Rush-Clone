using System.Collections;
using UnityEngine;

public class MoneyManager : Singleton<MoneyManager>
{
    public int totalValueOfMoney;

    private void Awake()
    {
        totalValueOfMoney = 0;
    }
}
