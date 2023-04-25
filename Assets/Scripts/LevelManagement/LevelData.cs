using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    private string levelKey = "level";
    private string moneyKey = "money";

    private int currentLevel;
    private int totalMoney;
    private int runtimeMoney;
    public int GetCurrentLevel() { return currentLevel; }

    private void Awake()
    {
        GetLevelData();
    }
    private void OnEnable()
    {
        LevelController.Instance.OnPlayerCollectedCollectible += OnPlayerCollectedCollectible;
        LevelController.Instance.OnPlayerCollidedWithObstacle += OnPlayerCollidedWithObstacle;
        LevelController.Instance.OnPlayerCollidedWithFinishLine += OnPlayerCollidedWithFinishLine;
    }


    private void OnDisable()
    {
        LevelController.Instance.OnPlayerCollectedCollectible -= OnPlayerCollectedCollectible;
        LevelController.Instance.OnPlayerCollidedWithObstacle -= OnPlayerCollidedWithObstacle;
        LevelController.Instance.OnPlayerCollidedWithFinishLine -= OnPlayerCollidedWithFinishLine;
    }

    private void OnPlayerCollidedWithFinishLine()
    {
        totalMoney += runtimeMoney;
        currentLevel++;
    }

    private void OnPlayerCollidedWithObstacle(Transform collidedObstacle)
    {
        
    }

    private void OnPlayerCollectedCollectible(CollectibleController collectibleController)
    {
        
    }
    private void GetLevelData()
    {
        if (PlayerPrefs.HasKey(levelKey))
        {
            currentLevel = PlayerPrefs.GetInt(levelKey);
        }
        else
        {
            currentLevel = 1;
        }

        if (PlayerPrefs.HasKey(moneyKey))
        {
            currentLevel = PlayerPrefs.GetInt(moneyKey);
        }
        else
        {
            totalMoney = 0;
        }
    }

    public int GetTotalMoney()
    {
        return totalMoney;
    }

}
