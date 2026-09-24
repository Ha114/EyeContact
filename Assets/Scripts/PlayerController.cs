using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] float walkSpeed = 20f;
    //[SerializeField] float runSpeed = 8f;
    //[SerializeField] float crouchSpeed = 2f;

    [Header("Jump and Fall")]
    //[SerializeField] private float jumpForce = 7f;
    [SerializeField] private float gravity = -12f;
    [SerializeField] private float initialFallVelocity = -2f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference interactAction;

    private CharacterController _characterController;
    private Vector2 _moveInput;
    private bool _isGrounded;
    private float _verticalVelocity;

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        moveAction.action.performed += StoreMovementInput;
        moveAction.action.canceled += StoreMovementInput;
        interactAction.action.performed += InteracteInput;
        interactAction.action.canceled += InteracteInput;

        //jumpAction.action.performed += Jump;
        //jumpAction.action.canceled += Jump;
    }

    private void OnDisable()
    {
        moveAction.action.performed -= StoreMovementInput;
        moveAction.action.canceled -= StoreMovementInput;
        interactAction.action.performed -= InteracteInput;
        interactAction.action.canceled -= InteracteInput;
        //jumpAction.action.performed -= Jump;
        //jumpAction.action.canceled -= Jump;
    }
    void Update()
    {
        _isGrounded = _characterController.isGrounded;
        HandleGravity();
        HandleMovement();
    }

    private void StoreMovementInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    
    //void Jump(InputAction.CallbackContext context)
    //{
    //    if (_isGrounded)
    //    {
    //        _verticalVelocity = jumpForce;
    //    }
    //}

    private void InteracteInput(InputAction.CallbackContext context)
    {
        Debug.Log("INTERACT: " + context.phase);
    }

    void HandleGravity()
    {
        if (_isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = initialFallVelocity;
        }

        _verticalVelocity += gravity * Time.deltaTime;
    }
    void HandleMovement()
    {
        var move = cameraTransform.TransformDirection(new Vector3(_moveInput.x, 0, _moveInput.y)).normalized;
        var currentSpeed = walkSpeed;
        var finalMove = move * currentSpeed;
        finalMove.y = _verticalVelocity;

        var collisions = _characterController.Move(finalMove * Time.deltaTime);
        if ((collisions & CollisionFlags.Above) != 0)
        {
            _verticalVelocity = initialFallVelocity;
        }
    }

}
