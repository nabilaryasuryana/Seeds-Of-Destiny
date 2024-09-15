using System.Collections;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Emote Animator")]
    [SerializeField] private Animator emoteAnimator;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON; // Hanya satu dialog

    [Header("Unique Dialogue ID")]
    [SerializeField] private string dialogueID; // ID unik untuk setiap dialog

    private bool playerInRange;
    public bool autoPlayOnEntered;
    public bool destroyable;

    protected virtual void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false); // Visual cue nonaktif di awal

        // Cek apakah SaveSystem sudah diinisialisasi
        if (SaveSystem.Instance != null)
        {
            // Jika dialog sudah diselesaikan sebelumnya, cek status dari sistem penyimpanan
            if (SaveSystem.Instance.IsDialogueCompleted(dialogueID))
            {
                Debug.Log("Dialogue with ID " + dialogueID + " is already completed.");
                if (destroyable)
                {
                    Destroy(gameObject); // Hancurkan GameObject jika destroyable diaktifkan
                }
            }
        }
        else
        {
            Debug.LogError("SaveSystem instance is not available.");
        }
    }

    public bool GetRangePlayer()
    {
        return playerInRange;
    }

    private void Update()
    {
        // Selalu aktifkan visual cue dan proses jika pemain berada dalam jangkauan
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true); // Tampilkan visual cue saat pemain dalam jangkauan

            if (autoPlayOnEntered)
            {
                StartDialogue();
            }

            // Tekan tombol interaksi untuk memulai dialog
            if (InputManager.GetInstance().GetInteractPressed())
            {
                StartDialogue();
            }
        }
        else
        {
            visualCue.SetActive(false); // Sembunyikan visual cue jika pemain tidak dalam jangkauan
        }
    }

    private void StartDialogue()
    {
        // Masukkan dialog ke DialogueManager dan mulai dialog
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator);
        CompleteDialogue(); // Tandai dialog selesai setelah dimulai
    }

    private void CompleteDialogue()
    {
        if (SaveSystem.Instance != null)
        {
            SaveSystem.Instance.MarkDialogueAsCompleted(dialogueID); // Tandai dialog selesai

            // Hancurkan objek ini jika destroyable diaktifkan
            if (destroyable)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogError("SaveSystem instance is not available.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Jika pemain masuk dalam jangkauan, set playerInRange menjadi true
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        // Jika pemain keluar dari jangkauan, set playerInRange menjadi false
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
