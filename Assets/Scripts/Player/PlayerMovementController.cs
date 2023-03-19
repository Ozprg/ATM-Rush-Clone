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

    private void Update()
    {
        PlayerMovement();
    }
    
    private void PlayerMovement()
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
