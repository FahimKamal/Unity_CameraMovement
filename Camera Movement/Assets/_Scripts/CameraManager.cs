using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    private Vector2 _delta;
    private bool _isMoving;
    private bool _isRotating;

    private float _xRotation;

    [SerializeField] private float cameraMoveSpeed = 10f;
    [SerializeField] private float cameraRotateSpeed = .5f;

    private void Awake()
    {
        _xRotation = transform.rotation.eulerAngles.x;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _delta = context.ReadValue<Vector2>();
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _isMoving = context.started || context.performed;
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        _isRotating = context.started || context.performed;
    }

    private void LateUpdate()
    {
        if (_isMoving)
        {
            var position = transform.right * (_delta.x * -cameraMoveSpeed);
            position += transform.forward * (_delta.y * -cameraMoveSpeed);
            transform.position += position * Time.deltaTime;
        }

        if (_isRotating)
        {
            transform.Rotate(new Vector3(_xRotation, -_delta.x * cameraRotateSpeed, 0.0f));
            transform.rotation = Quaternion.Euler(_xRotation, transform.rotation.eulerAngles.y, 0.0f);
        }
    }
    
} // class
