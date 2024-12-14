using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Require the Rigidbody2D component to be attached to the same GameObject
[RequireComponent(typeof(Rigidbody2D), typeof(Damageable))]
public class BatEnemy : MonoBehaviour
{
    // Variable to store Rigidbody2D component
    Rigidbody2D rb;

    Animator animator;

    public DetectionZone attackZone;

    Damageable damageable;
    // Variable to store the player's Damageable component
    Damageable playerDamageable;


    public bool _hasTarget = false;

    public bool HasTarget
    {
        get
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

}
