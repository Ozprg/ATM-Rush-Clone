public class PlayerController : Singleton<PlayerController>
{
    public PlayerMovementController playerMovementController { get; private set; }
    public PlayerCollisionController playerCollisionController { get; private set; }
    public StackManager stackManager { get; private set; }

    private void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerCollisionController = GetComponent<PlayerCollisionController>();
        stackManager = GetComponent<StackManager>();
    }
}
