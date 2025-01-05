using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private float typingTime = 0.05f;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

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
    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && !didDialogueStart)
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        playerController.DisableMovement();
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine()
    { 
        dialogueText.text = string.Empty;
        foreach (char ch in dialogueLines[lineIndex])
        { 
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void NextDialogueLine()
    { 
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            Destroy(gameObject);
            playerController.EnableMovement();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            if (damageable.IsPlayer)
            {
                isPlayerInRange = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            if (damageable.IsPlayer)
            {
                isPlayerInRange = false;
            }
        }
    }

    public void OnNextLine(InputAction.CallbackContext context)
    {
        if (context.started && isPlayerInRange && didDialogueStart && dialogueText.text == dialogueLines[lineIndex])
        {
            NextDialogueLine();
        }
    }
}
