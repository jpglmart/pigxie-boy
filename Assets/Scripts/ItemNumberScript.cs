using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemNumberScript : MonoBehaviour
{
    public TMP_Text itemNumberText;
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

    // Start is called before the first frame update
    void Start()
    {
        itemNumberText.text = "x" + playerController.ItemCount.ToString();
    }

    private void OnEnable()
    {
        playerController.itemCountChanged.AddListener(OnPlayerItemCountChanged);
    }
    private void OnDisable()
    {
        playerController.itemCountChanged.RemoveListener(OnPlayerItemCountChanged);
    }

    private void OnPlayerItemCountChanged(int itemCount)
    {
        itemNumberText.text = "x" + itemCount.ToString();
    }
}
