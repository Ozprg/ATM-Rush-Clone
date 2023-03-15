using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Move Referance")]
    [SerializeField] private Transform _transToMove;
    [SerializeField] private float xMin, xMax;
    private Vector3 firstPosition, endPositition, newPosition;
    private float _posX;

    [Header("Forward Move")]
    [SerializeField] private float _forwardMoveSpeed;

    private void Update()
    {
        PlayerMovement();
    }
    private void PlayerMovement()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            firstPosition = Input.mousePosition;
            _posX = _transToMove.localPosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            endPositition = Input.mousePosition;
            newPosition.x = ((endPositition.x - firstPosition.x) / (Screen.width / 30f)) + _posX;
            newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
            _transToMove.localPosition = new Vector3(newPosition.x, _transToMove.localPosition.y, _transToMove.localPosition.z);
            CollectibleManager1.Instance.MoveStackedElements();
        }

        if (Input.GetMouseButtonUp(0))
        {
            CollectibleManager1.Instance.MoveStackedElementsToOrigin();
        }
        
        transform.position += Vector3.forward * _forwardMoveSpeed * Time.deltaTime;
        
    }
    
}
