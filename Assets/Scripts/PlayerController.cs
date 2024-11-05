using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Require the Rigidbody2D component to be attached to the same GameObject
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    // Variable to store the move input
    Vector2 moveInput;
    // Variable to store the walk speed
    public float walkSpeed = 5f;
    // Variable to store the jump impulse
    public float jumpImpulse = 6.5f;
    // Reference to the Rigidbody2D component
    Rigidbody2D rb;
    // Reference to the Animator component
    Animator animator;
    // Variable to store if the player is moving [SerializedField to make it visible in the Inspector
    [SerializeField]
    private bool _isMoving = false;
    TouchingDirections touchingDirections;
    Damageable damageable;

    // Property to check if the player is moving
    public bool IsMoving { 
        get 
        {
            return _isMoving;
        } 
        private set 
        { 
            _isMoving = value;
            // Set animator isMoving parameter
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

    // Property to check if the player can move
    public bool CanMove { get
        { 
            return animator.GetBool(AnimationStrings.canMove);
        } 
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    // Property to get the current move speed
    public float CurrentMoveSpeed
    {
        get
        {
            // Animator canMove parameter is true
            if (CanMove)
            {
                // Player is not touching a wall
                if (!touchingDirections.IsOnWall)
                {
                    return walkSpeed;
                }
                // Player is touching a wall
                else
                {
                    return 0;
                }
            }
            // Animator locked movement
            else
            {
                return 0;
            }
        }
    }

    // Awake is called when PlayerController is initialized
    private void Awake()
    {
        // Get the reference to the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        // Get the reference to the Animator component
        animator = GetComponent<Animator>();
        // Get the reference to the TouchingDirections component
        touchingDirections = GetComponent<TouchingDirections>();
        // Get the reference to the Damageable component
        damageable = GetComponent<Damageable>();
    }

    // Frame-rate independent update method
    private void FixedUpdate()
    {
        if (!damageable.IsHit)
        {
            // Move the player according to its speed and move input
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
            // Update animator yVelocity parameter
            animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
        }
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
        // Get the move input value
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            // Check if the player is moving
            IsMoving = moveInput != Vector2.zero;
            // Set the facing direction of the player
            SetFacingDirection(moveInput);
        }
        else 
        {
            IsMoving = false;
        }
    }

    // Method to execute when Jump callback is triggered
    public void OnJump(InputAction.CallbackContext context)
    {
        
        if (context.started && touchingDirections.IsGrounded && CanMove && IsAlive)
        {
            // Set animator jump trigger
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            // Add an impulse to the player Rigidbody2D component
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        
        if (context.started && touchingDirections.IsGrounded  && IsAlive)
        {
            // Set animator attack trigger
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockBack)
    {
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }
}
