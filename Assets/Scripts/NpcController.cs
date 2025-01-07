using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NpcController : MonoBehaviour
{
    // Reference to the player GameObject
    GameObject player;
    // Reference to the PlayerController script
    PlayerController playerController;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("No player found in the scene, make sure player GameObject has tag 'Player' assigned");
        }
        else
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            if (damageable.IsPlayer)
            {
                // Collision detected with player
                playerController.Celebrate();
                // Invoke function after 5s
                Invoke("DelaySceneLoad", 5);
            }
        }
    }

    private void DelaySceneLoad()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");
    }
}
