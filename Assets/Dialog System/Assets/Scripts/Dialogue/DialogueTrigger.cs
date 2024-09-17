using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Emote Animator")]
    [SerializeField] private Animator emoteAnimator;

    [Header("Dialogue Playlist")]
    [SerializeField] private List<TextAsset> dialoguePlaylist; // Playlist dialog

    [Header("Unique Dialogue ID")]
    [SerializeField] private string dialogueID; // ID unik untuk setiap dialog

    public bool autoPlayOnEntered;
    public bool destroyable;

    private bool playerInRange;
    private bool dialoguePlayed; // Mencegah pengulangan dialog
    private bool canContinueToNextDialogue = false; // Flag untuk kontrol dialog berikutnya
    private int currentDialogueIndex; // Index untuk dialog saat ini

    protected virtual void Awake()
    {
        Debug.Log("Awake called.");
        playerInRange = false;
        visualCue.SetActive(false); // Visual cue nonaktif di awal

        LoadDialogueState(); // Muat status dialog dari SaveSystem
    }

    private void Start()
    {
        if (dialoguePlayed)
        {
            Debug.Log("Dialogue already played, preparing to start next dialogue.");
            dialoguePlayed = false; // Reset untuk memungkinkan dialog berikutnya dimulai
            StartDialogue(); // Langsung mulai dialog berikutnya jika sudah dimainkan
        }
    }

    private void LoadDialogueState()
    {
        Debug.Log("Loading dialogue state.");
        if (SaveSystem.Instance != null)
        {
            DialogueState savedState = SaveSystem.Instance.GetDialogueState(dialogueID);
            if (savedState != null)
            {
                currentDialogueIndex = savedState.currentDialogueIndex;
                dialoguePlayed = savedState.dialoguePlayed;

                Debug.Log($"Loaded state: Index={currentDialogueIndex}, Played={dialoguePlayed}");

                if (dialoguePlayed)
                {
                    // Jika dialog sudah dimainkan, langsung mulai dialog berikutnya
                    Debug.Log("Dialogue has been played. Moving to next dialogue.");
                    NextDialogue(); // Pindah ke dialog berikutnya
                    dialoguePlayed = false; // Reset flag untuk dialog berikutnya
                }

                if (destroyable)
                {
                    Debug.Log("Destroying GameObject as it is marked as destroyable.");
                    Destroy(gameObject); // Hancurkan GameObject jika destroyable diaktifkan
                }
            }
            else
            {
                Debug.Log("No saved state found. Initializing defaults.");
                dialoguePlayed = false; // Jika tidak ada data yang disimpan, set false agar bisa dimainkan
                currentDialogueIndex = 0; // Mulai dari dialog pertama dalam playlist
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
        Debug.Log("Update called.");
        // Aktifkan visual cue hanya jika pemain berada dalam jangkauan dan dialog belum dimainkan
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying && !dialoguePlayed)
        {
            visualCue.SetActive(true); // Tampilkan visual cue saat pemain dalam jangkauan

            if (autoPlayOnEntered)
            {
                Debug.Log("Auto-play on entered is enabled.");
                StartDialogue();
            }

            // Tekan tombol interaksi untuk memulai dialog
            if (InputManager.GetInstance().GetInteractPressed())
            {
                Debug.Log("Interact button pressed.");
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
        Debug.Log("Starting dialogue.");
        // Jika dialog sudah dimainkan, jangan mulai lagi
        if (dialoguePlayed)
        {
            Debug.Log("Dialogue already played. Skipping.");
            return;
        }

        // Mulai dialog dan tandai sebagai sudah dimainkan
        if (currentDialogueIndex < dialoguePlaylist.Count)
        {
            Debug.Log($"Starting dialogue at index: {currentDialogueIndex}");
            DialogueManager.GetInstance().EnterDialogueMode(dialoguePlaylist[currentDialogueIndex], emoteAnimator);
            dialoguePlayed = true;
            SaveDialogueState(); // Simpan status setelah memulai dialog

            if (autoPlayOnEntered)
            {
                Debug.Log("Completing dialogue as autoPlayOnEntered is enabled.");
                CompleteDialogue(); // Tandai dialog selesai secara otomatis jika autoPlayOnEntered aktif
            }
        }
        else
        {
            Debug.LogError("No dialogue available at the current index.");
        }
    }

    // Fungsi ini harus dipanggil untuk mengizinkan melanjutkan dialog berikutnya
    public void AllowNextDialogue()
    {
        Debug.Log("Allowing next dialogue.");
        NextDialogue();
        canContinueToNextDialogue = true;
    }

    // Fungsi ini dipanggil setelah dialog selesai
    private void CompleteDialogue()
    {
        Debug.Log("Completing dialogue.");
        if (canContinueToNextDialogue)
        {
            // Setelah dialog selesai, otomatis ke dialog berikutnya jika diizinkan
            NextDialogue();
        }

        if (destroyable && dialoguePlayed)
        {
            Debug.Log("Destroying GameObject as it is marked as destroyable.");
            Destroy(gameObject); // Hancurkan objek ini jika destroyable diaktifkan
        }
    }

    // Fungsi untuk melanjutkan ke dialog berikutnya dalam playlist
    private void NextDialogue()
    {
        Debug.Log("Moving to next dialogue.");
        if (currentDialogueIndex < dialoguePlaylist.Count - 1)
        {
            currentDialogueIndex++;
            dialoguePlayed = false; // Reset agar dialog berikutnya bisa dimainkan
            canContinueToNextDialogue = false; // Blokir hingga fungsi AllowNextDialogue dipanggil
            SaveDialogueState(); // Simpan status setelah pindah ke dialog berikutnya
            Debug.Log("Moved to next dialogue index: " + currentDialogueIndex);
        }
        else
        {
            Debug.Log("No more dialogues in the playlist.");
        }
    }

    // Simpan status dialog
    private void SaveDialogueState()
    {
        Debug.Log("Saving dialogue state.");
        if (SaveSystem.Instance != null)
        {
            DialogueState state = new DialogueState(dialogueID, currentDialogueIndex, dialoguePlayed);
            SaveSystem.Instance.SaveDialogueState(dialogueID, state); // Simpan state dialog
        }
        else
        {
            Debug.LogError("SaveSystem instance is not available.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Trigger entered.");
        // Jika pemain masuk dalam jangkauan, set playerInRange menjadi true
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("Trigger exited.");
        // Jika pemain keluar dari jangkauan, set playerInRange menjadi false
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
