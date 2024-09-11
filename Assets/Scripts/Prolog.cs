using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Tambahkan ini untuk komponen UI Image

public class Prolog : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Komponen TextMeshPro untuk menampilkan teks
    public string[] lines; // Array berisi dialog yang akan ditampilkan
    public float textSpeed; // Kecepatan penampilan tiap huruf
    public Image backgroundImage; // Komponen Image untuk background
    public Sprite[] backgroundImages; // Array untuk background sesuai dialog
    public CanvasGroup backgroundCanvasGroup; // CanvasGroup untuk fade effect
    public float fadeDuration = 1f; // Durasi fade in dan fade out
    public int[] imageChangeIndices; // Array berisi index dialog yang mengubah gambar

    private int index; // Indeks untuk melacak baris dialog saat ini
    private int currentBackgroundIndex; // Indeks untuk melacak background saat ini

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty; // Mengosongkan teks di awal
        currentBackgroundIndex = 0; // Set background pertama
        backgroundImage.sprite = backgroundImages[currentBackgroundIndex]; // Atur background awal
        backgroundCanvasGroup.alpha = 1f; // Set alpha ke 1 (sepenuhnya terlihat)
        StartDialogue(); // Memulai dialog
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Input mouse untuk melanjutkan dialog
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
                    StartCoroutine(FadeBackground()); // Lakukan fade out dan fade in
                }
            }

            StartCoroutine(TypeLine()); // Mulai menampilkan baris baru
        }
        else
        {
            gameObject.SetActive(false); // Sembunyikan objek jika semua baris telah selesai
        }
    }

    IEnumerator FadeBackground()
    {
        // Fade out
        yield return StartCoroutine(FadeOut());

        // Ganti gambar background setelah fade out selesai
        backgroundImage.sprite = backgroundImages[currentBackgroundIndex];

        // Fade in
        yield return StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            backgroundCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        backgroundCanvasGroup.alpha = 0f; // Pastikan alpha benar-benar 0 setelah fade out
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            backgroundCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        backgroundCanvasGroup.alpha = 1f; // Pastikan alpha benar-benar 1 setelah fade in
    }
}
