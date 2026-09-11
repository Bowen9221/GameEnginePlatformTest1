using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputSystem_Manager : MonoBehaviour
{
    public Action<Vector2> OnMoveInput;
    public Action<Vector2> OnLookInput;
    public Action OnJumpAction;
    public Action OnInteractStarted;
    public Action OnInteractCanceled;

    public InputActionAsset _inputControls;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _interactAction;
    private InputAction _jumpAction;

    private void Awake()
    {
        var map = _inputControls.FindActionMap("Player");
        _moveAction = map.FindAction("Move");
        _lookAction = map.FindAction("Look");
        _interactAction = map.FindAction("Interact");
        _jumpAction = InputSystem.actions.FindAction("Jump");

        _interactAction.started += ctx => OnInteractStarted?.Invoke();
        _interactAction.canceled += ctx => OnInteractCanceled?.Invoke();

        _jumpAction.performed += ctx => OnJumpAction?.Invoke();
    }

    private void Update()
    {
        Vector2 moveDir = _moveAction.ReadValue<Vector2>();
        OnMoveInput?.Invoke(moveDir);

        Vector2 lookDir = _lookAction.ReadValue<Vector2>();
        OnLookInput?.Invoke(lookDir);

        
    }

    void OnEnable() => _inputControls.Enable();
    void OnDisable() => _inputControls.Disable();


}
