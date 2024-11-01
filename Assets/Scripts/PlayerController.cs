using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Require the Rigidbody2D component to be attached to the same GameObject
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // Variable to store the move input
    Vector2 moveInput;
    // Variable to store the walk speed
    public float walkSpeed = 5f;
    // Reference to the Rigidbody2D component
    Rigidbody2D rb;
    // Reference to the Animator component
    Animator animator;
    // Variable to store if the player is moving [SerializedField to make it visible in the Inspector
    [SerializeField]
    private bool _isMoving = false;

    // Property to check if the player is moving
    public bool IsMoving { 
        get 
        {
            return _isMoving;
        } 
        private set 
        { 
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        } 
    }
    // Variable to store if the player is facing right
    private bool _isFacingRight = true;
    // Property to check if the player is facing right
    public bool IsFacingRight { 
        get 
        {
            return _isFacingRight;
        } 
        private set 
        {
            if (_isFacingRight != value) 
            {
                // Flip the player sprite horizontally
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        } 
    }

    // Awake is called when PlayerController is initialized
    private void Awake()
    {
        // Get the reference to the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Frame-rate independent update method
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
    }

    // Method to set the facing direction of the player depending on the move input
    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight) {
            IsFacingRight = false;
        }
    }

    // Method to execute when Move callback is triggered
    public void OnMove(InputAction.CallbackContext context)
    { 
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }
}
