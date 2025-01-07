using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    public int attackDamage = 34;
    public Vector2 moveSpeed = new Vector2(5f, 0);
    public Vector2 knockBack = new Vector2(5, 2);
    // Reference to the item audio source
    AudioSource itemAudioSource;
    public float volume = 1f;
    // Reference to the player GameObject
    GameObject player;
    // Reference to the PlayerController script
    PlayerController playerController;
    TouchingDirections touchingDirections;

    [SerializeField]
    private bool _isMoving = true;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            _isMoving = value;
        }
    }

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("No player found in the scene, make sure player GameObject has tag 'Player' assigned");
        } 
        else
        {
            playerController = player.GetComponent<PlayerController>();
        }
        // Get the reference to the TouchingDirections component
        touchingDirections = GetComponent<TouchingDirections>();

        itemAudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (IsMoving)
        {
            rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
        }
    }

    // Frame-rate independent update method
    private void FixedUpdate()
    {
        if (gameObject != null && rb != null && IsMoving)
        {
            // Rotate the item as it moves horizontally
            rb.rotation += 25f;
        }

        if (touchingDirections.IsGrounded || touchingDirections.IsOnWall)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 directionKnocback = transform.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);
            bool gotHit = false; 
            if (!damageable.IsPlayer)
            {
                // Collision detected with an enemy
                gotHit = damageable.Hit(attackDamage, directionKnocback);
            }
            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + attackDamage);
                Destroy(gameObject);
            }
            else if (!IsMoving) 
            {
                // Collision detected with player
                Debug.Log(collision.name + " picked an item");
                if (itemAudioSource)
                {
                    PlayClipAt(itemAudioSource.clip, gameObject.transform.position, volume);
                }
                playerController.ItemCount++;
                Destroy(gameObject);
            }

        }
    }
    AudioSource PlayClipAt(AudioClip clip, Vector3 pos, float volume)
    {
        GameObject tempGO = new GameObject("TempAudio"); // create the temp object
        tempGO.transform.position = pos; // set its position
        AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
        aSource.clip = clip; // define the clip
        aSource.volume = volume; // set the volume
        aSource.Play(); // start the sound
        Destroy(tempGO, clip.length); // destroy object after clip duration
        return aSource; // return the AudioSource reference
    }
}
