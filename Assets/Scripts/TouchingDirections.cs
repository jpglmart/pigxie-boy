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

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Frame-rate independent update method
    void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;   
    }
}
