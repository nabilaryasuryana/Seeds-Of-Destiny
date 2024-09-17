using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Tambahkan ini untuk komponen UI Image
using UnityEngine.SceneManagement; // Untuk perpindahan scene

public class Prolog : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Komponen TextMeshPro untuk menampilkan teks
    public string[] lines; // Array berisi dialog yang akan ditampilkan
    public float textSpeed; // Kecepatan penampilan tiap huruf
    public Image backgroundImage; // Komponen Image untuk background
    public Sprite[] backgroundImages; // Array untuk background sesuai dialog
    public int[] imageChangeIndices; // Array berisi index dialog yang mengubah gambar
    public string sceneToLoad; // Nama scene yang akan diload setelah dialog selesai

    private int index; // Indeks untuk melacak baris dialog saat ini
    private int currentBackgroundIndex; // Indeks untuk melacak background saat ini

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty; // Mengosongkan teks di awal
        currentBackgroundIndex = 0; // Set background pertama
        backgroundImage.sprite = backgroundImages[currentBackgroundIndex]; // Atur background awal

        StartDialogue(); // Memulai dialog
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) // Input tombol "Jump" untuk melanjutkan dialog
        {
            if (textComponent.text == lines[index])
            {
                NextLine(); // Jika teks sudah tampil penuh, lanjut ke baris berikutnya
            }
            else
            {
                StopAllCoroutines(); // Jika belum selesai mengetik, hentikan Coroutine
                textComponent.text = lines[index]; // Tampilkan seluruh baris secara instan
            }
        }
    }

    void StartDialogue()
    {
        index = 0; // Mulai dari baris dialog pertama
        StartCoroutine(TypeLine()); // Memulai coroutine untuk menampilkan teks satu per satu
    }

    IEnumerator TypeLine()
    {
        // Mengambil setiap karakter dalam string dari array `lines` berdasarkan indeks
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c; // Menambah karakter ke teks yang ada
            yield return new WaitForSeconds(textSpeed); // Menunggu sesuai dengan `textSpeed`
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++; // Pindah ke baris berikutnya
            textComponent.text = string.Empty; // Kosongkan teks sebelum menampilkan yang baru

            // Cek apakah indeks saat ini ada di array `imageChangeIndices`
            if (System.Array.Exists(imageChangeIndices, element => element == index))
            {
                if (currentBackgroundIndex < backgroundImages.Length - 1)
                {
                    currentBackgroundIndex++; // Pindah ke background berikutnya
                    backgroundImage.sprite = backgroundImages[currentBackgroundIndex]; // Ganti background tanpa fade
                }
            }

            StartCoroutine(TypeLine()); // Mulai menampilkan baris baru
        }
        else
        {
            // Jika dialog selesai, langsung pindah ke scene tanpa fade
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
