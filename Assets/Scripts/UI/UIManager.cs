using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyIncreaseText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI gameStarterText ;

    private void OnEnable()
    {
        LevelController.Instance.OnLevelIsCreated += OnLevelIsCreated;
    }

    private void OnDisable()
    {
        LevelController.Instance.OnLevelIsCreated -= OnLevelIsCreated;
    }
    
    private void OnLevelIsCreated()
    {
        gameStarterText.text = "Tap To Start";
        moneyText.text = LevelController.Instance.levelData.GetTotalMoney().ToString();
        LevelText.text = LevelController.Instance.levelData.GetCurrentLevel().ToString();
    }
}
 