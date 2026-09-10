using UnityEngine;

public class Character_Movement_Handler : MonoBehaviour
{

    [Header("Movement Variables")]
    //-----------------------//
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    public bool jumped = false;

    Vector3 _moveInput;




    [Header("applied Forces")]
    private Vector3 _verticalVelocity;
    private float _gravity = -9.81f;

    [SerializeField] float accelleration;
    [SerializeField] float deceleration;
    private Vector3 _currentHorizontalVelocity;

    [Header("References")]

    CharacterController _characterController;
    [SerializeField] InputSystem_Manager isManager;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        isManager.OnMoveInput += ReceiveMoveInput;
    }

    private void OnDisable()
    {
        isManager.OnMoveInput -= ReceiveMoveInput;
        _moveInput = Vector2.zero;
    }
    void ReceiveMoveInput(Vector2 MoveInput)
    {
        _moveInput = MoveInput;
        
    }



    private void Update()
    {
        HandleMovement();
        HandleGravity();
        HandleJump();
    }

    void HandleMovement()
    {
        Vector3 moveDir = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        Vector3 targetVelocity = moveDir.normalized * moveSpeed;

        float speedChangeRate = (moveDir.sqrMagnitude > 0.001) ? accelleration : deceleration;
        _currentHorizontalVelocity = Vector3.Lerp(_currentHorizontalVelocity, targetVelocity, speedChangeRate * Time.deltaTime);

        
        
        

        _characterController.Move(_currentHorizontalVelocity * Time.deltaTime);

       
    }

    void HandleGravity()
    {
        // using to reset so that gravity isn't constantly adding to _vV.y
        if (_characterController.isGrounded && _verticalVelocity.y < 0)
        {
            _verticalVelocity.y = -2f;
        }

        // Add gravity over time
        _verticalVelocity.y += _gravity * Time.deltaTime;

        // Apply vertical force
        _characterController.Move(_verticalVelocity * Time.deltaTime);
    }

    void HandleJump()
    {
        if (_characterController.isGrounded && jumped)
        {
            Vector3 JumpForce = new(0, 1, 0);

            _verticalVelocity.y = JumpForce.y * jumpForce;
        }
    }




}
