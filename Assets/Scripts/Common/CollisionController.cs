using System;
using UnityEngine;

public abstract class CollisionController : MonoBehaviour
{
    protected bool canDetectCollision;
    
    public const string interactableTag = "Interactable";
    public const string layerCollectible = "Collectible";
    public const string layerObstacle = "Obstacle";
    public const string layerGate = "Gate";
    public const string layerATM = "ATM";
    public const string layerFinishLine = "Finish";
    public const string layerPlayer = "Player";

    public int collectibleLayer { get; private set; }
    public int obstacleLayer { get; private set; }
    public int gateLayer { get; private set; }
    public int ATMlayer { get; private set; }
    public int FinishLineLayer { get; private set; }
    public int PlayerLayer { get; private set; }


    private void Awake()
    {
        collectibleLayer = LayerMask.NameToLayer(layerCollectible);
        obstacleLayer = LayerMask.NameToLayer(layerObstacle);
        gateLayer = LayerMask.NameToLayer(layerGate);
        ATMlayer = LayerMask.NameToLayer(layerATM);
        FinishLineLayer = LayerMask.NameToLayer(layerFinishLine);
        PlayerLayer = LayerMask.NameToLayer(layerPlayer);
    }

    private void OnEnable()
    {
        LevelController.Instance.OnFirstInputDetected += EnableCollisionDetection;
    }
    
    private void OnDisable()
    {
        LevelController.Instance.OnFirstInputDetected -= EnableCollisionDetection;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other != null)
        {
            DetectCollision(other);
        }
    }
    
    private void EnableCollisionDetection()
    {
        canDetectCollision = true;
    }
    
    protected abstract void DetectCollision(Collision other);
}