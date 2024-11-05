using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Uses the collider to check if the player is touching the ground or touching the wall
public class TouchingDirections : MonoBehaviour
{
    // Variable to store ContactFilter2D component
    public ContactFilter2D castFilter;
    CapsuleCollider2D touchingCol;
    // Variable to store Animator component
    Animator animator;
    // Array to store the ground hits
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    // Variable to store the ground distance
    public float groundDistance = 0.05f;
    // Array to store the wall hits
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    // Variable to store the wall distance
    public float wallDistance = 0.2f;
    // Array to store the ceiling hits
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    // Variable to store the ceiling distance
    public float ceilingDistance = 0.05f;

    // Variable to store if the player is on the ground [SerializedField to make it visible in the Inspector
    [SerializeField]
    private bool _isGrounded;
    // Property to check if the player is on the ground
    public bool IsGrounded { 
        get 
        {
            return _isGrounded;
        }
        private set 
        {
            _isGrounded = value;
            // Set animator isGrounded parameter
            animator.SetBool(AnimationStrings.isGrounded, value);
        } 
    }

    // Variable to store if the player is touching a wall [SerializedField to make it visible in the Inspector
    [SerializeField]
    private bool _isOnWall;
    // Property to check if the player is touching a wall
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            // Set animator isOnWall parameter
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    // Variable to store the wall check direction
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;


    // Variable to store if the player is touching the ceiling [SerializedField to make it visible in the Inspector
    [SerializeField]
    private bool _isOnCeiling;
    // Property to check if the player is touching the ceiling
    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            // Set animator isOnCeiling parameter
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }
    // Awake is called when TouchingDirections is initialized
    private void Awake()
    {
        // Get the CapsuleCollider2D component
        touchingCol = GetComponent<CapsuleCollider2D>();
        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    // Frame-rate independent update method
    void FixedUpdate()
    {
        // Check if the player is touching the ground
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        // Check if the player is touching the wall
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        // Check if the player is touching the ceiling
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
