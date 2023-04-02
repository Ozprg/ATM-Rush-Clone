using DG.Tweening;
using UnityEngine;
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    private Vector3 firstPosition;
    private Vector3 endPosition;
    private Vector3 newPosition;
    private float _posX;
    private bool playerMovementEnabled;
        
    private void Awake()
    {
        playerMovementEnabled = true;
    }
    private void Update()
    {
        PlayerMovement();
    }
    
    private void PlayerMovement()
    {
        if (playerMovementEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstPosition = Input.mousePosition;
                _posX = transform.localPosition.x;
            }

            if (Input.GetMouseButton(0))
            {
                endPosition = Input.mousePosition;
                newPosition.x = ((endPosition.x - firstPosition.x) / (Screen.width / 30f)) + _posX;
                newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
                transform.localPosition = new Vector3(newPosition.x, transform.localPosition.y, transform.localPosition.z);
            }

            transform.position += Vector3.forward * _moveSpeed * Time.deltaTime;
        }
    }
        
    public void OnFinishedStopPlayerMovement()
    {
        playerMovementEnabled = false;
        Transform _levelPlatform = LevelController.Instance.creator.CurrentLevelPlatform;
        float _maxZPoint = _levelPlatform.position.z + (_levelPlatform.localScale.z / 2);
        transform.DOMove(new Vector3 (0,transform.position.y, _maxZPoint), 0.6f);
    }

}
