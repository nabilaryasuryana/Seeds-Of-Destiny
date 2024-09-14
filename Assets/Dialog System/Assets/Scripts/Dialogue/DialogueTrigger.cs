using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Emote Animator")]
    [SerializeField] private Animator emoteAnimator;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Unique Dialogue ID")]
    [SerializeField] private string dialogueID; // ID unik untuk setiap dialog

    private bool playerInRange;
    public bool autoPlayOnEntered;

    protected virtual void Awake() 
    {
        playerInRange = false;
        visualCue.SetActive(false);

        // Cek jika dialog ini sudah selesai
        if (SaveSystem.Instance.IsDialogueCompleted(dialogueID))
        {
            Destroy(gameObject); // Hancurkan GameObject jika dialog sudah selesai
        }
    }

    // Ubah dari private menjadi protected atau public
    public bool GetRangePlayer()
    {
        return playerInRange;
    }


    private void Update() 
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying) 
        {
            if (autoPlayOnEntered)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator);
                CompleteDialogue(); // Tandai dialog selesai saat dijalankan
            }

            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed()) 
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator);
                CompleteDialogue(); // Tandai dialog selesai saat dijalankan
            }
        }
        else 
        {
            visualCue.SetActive(false);
        }
    }

    private void CompleteDialogue()
    {
        SaveSystem.Instance.MarkDialogueAsCompleted(dialogueID); // Tandai dialog sebagai selesai
        Destroy(gameObject); // Hancurkan GameObject ini
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
