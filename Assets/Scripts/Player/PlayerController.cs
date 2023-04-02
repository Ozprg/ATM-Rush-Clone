using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public PlayerMovementController playerMovementController { get; private set; }
    public PlayerCollisionController playerCollisionController { get; private set; }
    public StackManager stackManager { get; private set; }

    public bool collisionWithPlayerDetected;

    private void OnEnable()
    {
        LevelController.Instance.OnPlayerCollidedWithObstacle += OnPlayerCollidedWithObstacle;
        LevelController.Instance.OnPlayerCollidedWithFinishLine += OnPlayerCollidedWithFinishLine;
;
    }

    private void OnDisable()
    {
        LevelController.Instance.OnPlayerCollidedWithObstacle -= OnPlayerCollidedWithObstacle;
        LevelController.Instance.OnPlayerCollidedWithFinishLine -= OnPlayerCollidedWithFinishLine;
    }
    private void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerCollisionController = GetComponent<PlayerCollisionController>();
        stackManager = GetComponent<StackManager>();
    }

    public void OnPlayerCollidedWithObstacle(Transform playerTransform)
    {
        playerTransform.position = Vector3.Lerp(playerTransform.position, playerTransform.position - new Vector3(0,0,8), 100f);
        collisionWithPlayerDetected = true;
    }

    public void  OnPlayerCollidedWithFinishLine()
    {
        playerMovementController.OnFinishedStopPlayerMovement();
    }
}
