using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    private Vector2 _delta;
    private Vector2 _screenPosition;
    private bool _isMoving;
    private bool _isRotating;

    private float _xRotation;

    [SerializeField] private float cameraMoveSpeed = 10f;
    [SerializeField] private float cameraRotateSpeed = .5f;
    [SerializeField] private Text objNameDisplay;
    [SerializeField] private LayerMask clickableLayer;

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

    public void OnScreenPositionChange(InputAction.CallbackContext context)
    {
        _screenPosition = context.ReadValue<Vector2>();
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        var ray = Camera.main.ScreenPointToRay(new Vector3(_screenPosition.x, _screenPosition.y, 0.0f));
        if (Physics.Raycast(ray, out var hit, float.MaxValue, clickableLayer))
        {
            if (hit.transform.TryGetComponent<Clickable>(out var obj))
            {
                Debug.Log(obj.name);
                objNameDisplay.text = obj.name;
            }
            else
            {
                Debug.Log("nothing");
                objNameDisplay.text = "nothing";
            }
        }

    }

    private void LateUpdate()
    {
        if (_isMoving)
        {
            // Debug.Log(_delta);
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
