using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    public LevelCreator creator { get; private set; }

    private void Awake()
    {
        creator = GetComponent<LevelCreator>();
    }

    #region Events and Delegates

    // Level

    public delegate void OnLevelFailedDelegate();
    public event OnLevelFailedDelegate OnLevelFailed;

    public delegate void OnLevelCompletedDelegate(CollectibleController collectibleController);
    public event OnLevelCompletedDelegate OnLevelCompleted;

    // In Game

    public delegate void OnLevelIsCreatedDelegate();
    public event OnLevelIsCreatedDelegate OnLevelIsCreated;

    public delegate void OnPlayerCollectedCollectibleDelegate(CollectibleController collectibleController);
    public event OnPlayerCollectedCollectibleDelegate OnPlayerCollectedCollectible;

    public delegate void OnPlayerCollidedWithObstacleDelegate(Transform collidedObstacle);
    public event OnPlayerCollidedWithObstacleDelegate OnPlayerCollidedWithObstacle;

    public delegate void OnPlayerCollidedWithFinishLineDelegate();
    public event OnPlayerCollidedWithFinishLineDelegate OnPlayerCollidedWithFinishLine;

    public delegate void OnStackedObjectCollidedWithFinishLineDelegate(CollectibleController collectibleController);
    public event OnStackedObjectCollidedWithFinishLineDelegate OnStackedObjectCollidedWithFinishLine;

    public delegate void OnStackedObjectHitObstacleDelegate(CollectibleController stackedObject);
    public event OnStackedObjectHitObstacleDelegate OnStackedObjectHitObstacle;

    public delegate void OnFirstInputDetectedDelegate();
    public event OnFirstInputDetectedDelegate OnFirstInputDetected;

    public delegate void OnPlayerTouchedGateDelegate(CollectibleController collectibleController);
    public event OnPlayerTouchedGateDelegate OnPlayerTouchedGate;
    
    public delegate void OnCollectibleTouchedATMDelegate(CollectibleController collectibleController, ATMController aTMController);
    public event OnCollectibleTouchedATMDelegate OnCollectibleTouchedATM;

    public delegate void OnCollectibleSoldDelegate(CollectibleController collectibleController);
    public event OnCollectibleSoldDelegate OnCollectibleSold;

    #endregion

    #region Invoke Methods
    public void LevelFailed()
    {
        OnLevelFailed?.Invoke();
    }

    public void LevelCompleted(CollectibleController collectibleController)
    {
        OnLevelCompleted?.Invoke(collectibleController);
    }

    public void LevelIsCreated()
    {
        OnLevelIsCreated?.Invoke();
    }

    public void PlayerCollectedCollectible(CollectibleController collectibleController)
    {
        OnPlayerCollectedCollectible?.Invoke(collectibleController);
    }

    public void PlayerCollidedWithObstacle(Transform collidedObstacle)
    {
        OnPlayerCollidedWithObstacle?.Invoke(collidedObstacle);
    }

    public void StackedObjectHitObstacle(CollectibleController stackedObject)
    {
        OnStackedObjectHitObstacle?.Invoke(stackedObject);
    }

    public void FirstInputDetected()
    {
        OnFirstInputDetected?.Invoke();
    }

    public void PlayerTouchedGate(CollectibleController collectibleController)
    {
        OnPlayerTouchedGate?.Invoke(collectibleController);
    }

    public void CollectibleTouchedATM(CollectibleController collectibleController, ATMController aTMController)
    {
        OnCollectibleTouchedATM?.Invoke(collectibleController, aTMController);
    }

    public void CollectibleSold(CollectibleController collectibleController)
    {
        OnCollectibleSold?.Invoke(collectibleController);
    }

    public void PlayerCollidedWithFinishLine()
    {
        OnPlayerCollidedWithFinishLine.Invoke();
    }

    public void StackedObjectCollidedWithFinishLine(CollectibleController collectibleController)
    {
        OnStackedObjectCollidedWithFinishLine?.Invoke(collectibleController);
    }

    #endregion
}