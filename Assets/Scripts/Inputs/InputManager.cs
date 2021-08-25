using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    public UnityAction<Vector2, float> OnStartTouch;
    public UnityAction<Vector2, float> OnEndTouch;
    
    private PlayerControls _playerControls;
    private Camera _mainCamera;

    public Vector2 PrimaryPosition 
        => Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimatyPosition.ReadValue<Vector2>());    

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    void Start()
    {
        _playerControls.Touch.PrimaryContact.started += StartTouchPrimary;
        _playerControls.Touch.PrimaryContact.canceled += EndTouchPrimary;
    }

    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnStartTouch?.Invoke(Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimatyPosition.ReadValue<Vector2>()), (float) ctx.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnEndTouch?.Invoke(Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimatyPosition.ReadValue<Vector2>()), (float) ctx.time);
    }
}
