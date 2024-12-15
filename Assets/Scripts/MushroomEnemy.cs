using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Require the Rigidbody2D component to be attached to the same GameObject
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class MushroomEnemy : MonoBehaviour
{
    // Variable to store the walk speed
    public float walkSpeed = 3f;
    // Variable to store Rigidbody2D component
    Rigidbody2D rb;

    Animator animator;

    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    TouchingDirections touchingDirections;
    Damageable damageable;
    // Variable to store the player's Damageable component
    Damageable playerDamageable;

    public enum WalkableDirection { Right, Left };

    private WalkableDirection _walkDirection;

    private Vector2 walkDirectionVector = Vector2.right;


    public WalkableDirection WalkDirection
    {
        get 
        { 
            return _walkDirection;
        }
        set
        {
            if (_walkDirection != value) 
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;

    public bool CanMove 
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool HasTarget { get 
        {
            return _hasTarget;
        } 
        set 
        {
            if (playerDamageable != null && playerDamageable.IsAlive)
            {
                _hasTarget = value;
                animator.SetBool(AnimationStrings.hasTarget, value);
            }
            else
            {
                _hasTarget = false;
                animator.SetBool(AnimationStrings.hasTarget, false);
            }
        } 
    }

    // Awake is called when PlayerController is initialized
    public void Awake() 
    {
        // Get the reference to the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        // Get the reference to the TouchingDirections component
        touchingDirections = GetComponent<TouchingDirections>();
        // Get the reference to the Animator component
        animator = GetComponent<Animator>();
        // Get the reference to the Damageable component
        damageable = GetComponent<Damageable>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("No player found in the scene, make sure player GameObject has tag 'Player' assigned");
        }
        playerDamageable = player.GetComponent<Damageable>();
    }
    // Update is called once per frame
    void Update()
    { 
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    // Frame-rate independent update method
    public void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && (touchingDirections.IsOnWall || cliffDetectionZone.detectedColliders.Count == 0))
        {
            FlipDirection();
        }
        if (CanMove && damageable.IsAlive)
        {
            // Move the enemy according to its speed
            rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
        } 
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
    }


    private void FlipDirection()
    {


        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to right or left");
        }
    }

    public void OnHit(int damage, Vector2 knockBack)
    {
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }
}
