using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Uses the collider to check if the player is touching the ground or touching the wall
public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    CapsuleCollider2D touchingCol;

    Animator animator;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    public float groundDistance = 0.05f;

    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    public float wallDistance = 0.2f;

    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    public float ceilingDistance = 0.05f;

    [SerializeField]
    private bool _isGrounded;

    public bool IsGrounded { 
        get 
        {
            return _isGrounded;
        }
        private set 
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        } 
    }

    [SerializeField]
    private bool _isOnWall;

    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    [SerializeField]
    private bool _isOnCeiling;

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0? Vector2.right : Vector2.left;

    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Frame-rate independent update method
    void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;   
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
