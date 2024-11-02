using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Require the Rigidbody2D component to be attached to the same GameObject
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class MushroomEnemy : MonoBehaviour
{
    // Variable to store the walk speed
    public float walkSpeed = 3f;
    // Variable to store Rigidbody2D component
    Rigidbody2D rb;

    TouchingDirections touchingDirections;

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

    // Awake is called when PlayerController is initialized
    public void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
    }
    // Frame-rate independent update method
    public void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        // Move the enemy according to its speed
        rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
        
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
}
