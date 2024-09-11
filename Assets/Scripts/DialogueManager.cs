using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Komponen TextMeshPro untuk menampilkan teks
    public string[] lines; // Array berisi dialog yang akan ditampilkan
    public float textSpeed; // Kecepatan penampilan tiap huruf
    public GameObject dialogueBox; // Objek dialog UI

    private int index; // Indeks untuk melacak baris dialog saat ini

    void Start()
    {
        dialogueBox.SetActive(false); // Sembunyikan dialog box di awal
        textComponent.text = string.Empty; // Kosongkan teks di awal
    }

    public void StartDialogue()
    {
        dialogueBox.SetActive(true); // Tampilkan dialog box
        index = 0; // Mulai dari baris pertama
        StartCoroutine(TypeLine()); // Memulai coroutine untuk menampilkan teks
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1) // Jika masih ada dialog
        {
            index++;
            textComponent.text = string.Empty; // Kosongkan teks untuk baris berikutnya
            StartCoroutine(TypeLine()); // Mulai baris berikutnya
        }
        else
        {
            EndDialogue(); // Akhiri dialog jika sudah selesai
        }
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false); // Sembunyikan kotak dialog setelah dialog selesai
        textComponent.text = string.Empty; // Kosongkan teks
    }
}
