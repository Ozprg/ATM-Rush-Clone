using System;
using UnityEngine;

public abstract class CollisionController : MonoBehaviour
{
    protected bool canDetectCollision;
    
    public const string interactableTag = "Interactable";
    public const string layerCollectible = "Collectible";
    public const string layerObstacle = "Obstacle";

    public int collectibleLayer { get; private set; }
    public int obstacleLayer { get; private set; }

    private void Awake()
    {
        collectibleLayer = LayerMask.NameToLayer(layerCollectible);
        obstacleLayer = LayerMask.NameToLayer(layerObstacle);
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