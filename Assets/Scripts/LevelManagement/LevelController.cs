using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    // Level
    public delegate void OnLevelFailedDelegate();
    public event OnLevelFailedDelegate OnLevelFailed;
    public delegate void OnLevelCompletedDelegate();
    public event OnLevelCompletedDelegate OnLevelCompleted;

    // In Game
    public delegate void OnLevelIsCreatedDelegate();
    public event OnLevelIsCreatedDelegate OnLevelIsCreated;

    public delegate void OnPlayerCollectedCollectibleDelegate(CollectibleController collectibleController);
    public event OnPlayerCollectedCollectibleDelegate OnPlayerCollectedCollectible;
    public delegate void OnPlayerCollidedWithObstacleDelegate(Transform collidedObstacle);
    public event OnPlayerCollidedWithObstacleDelegate OnPlayerCollidedWithObstacle;
    public delegate void OnStackedObjectHitObstacleDelegate(CollectibleController stackedObject);
    public event OnStackedObjectHitObstacleDelegate OnStackedObjectHitObstacle;
    public delegate void OnFirstInputDetectedDelegate();
    public event OnFirstInputDetectedDelegate OnFirstInputDetected;
    public delegate void OnPlayerTouchedGateDelegate(CollectibleController collectibleController);
    public event OnPlayerTouchedGateDelegate OnPlayerTouchedGate;

    public void LevelFailed()
    {
        OnLevelFailed?.Invoke();
    }

    public void LevelCompleted()
    {
        OnLevelCompleted?.Invoke();
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
}