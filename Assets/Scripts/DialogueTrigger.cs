using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager; // Referensi ke DialogueManager

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Pastikan hanya player yang memicu trigger
        {
            dialogueManager.StartDialogue(); // Mulai dialog ketika player mendekati NPC
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Ketika player meninggalkan area trigger
        {
            dialogueManager.EndDialogue(); // Akhiri dialog
        }
    }
}
