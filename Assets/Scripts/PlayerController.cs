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

    // Property to check if the player is moving
    public bool IsMoving { get; private set; }

    // Awake is called when PlayerController is initialized
    private void Awake()
    {
        // Get the reference to the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
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

    // Method to execute when Move callback is triggered
    public void OnMove(InputAction.CallbackContext context)
    { 
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
    }
}
