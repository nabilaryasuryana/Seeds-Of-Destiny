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

    private bool playerInRange;
    public bool autoPlayOnEntered;

    protected virtual void Awake() 
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

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
                Destroy(this);
            }
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed()) 
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator);
            }
        }
        else 
        {
            visualCue.SetActive(false);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
