using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TombInfo : DialogueTrigger
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f; // Durasi efek fade

    private bool isFading = false;

    protected override void Awake()
    {
        base.Awake();   
    }
    private void Start()
    {
        // Set canvas alpha ke 0 agar tidak terlihat di awal
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void FixedUpdate()
    {
        // Gunakan metode GetRangePlayer untuk mendapatkan status player dalam jangkauan
        bool inRange = GetRangePlayer(); 

        // Jika player dalam jangkauan dan tidak sedang fading, lakukan fade in
        if (inRange && !isFading)
        {
            StartCoroutine(FadeCanvas(1f)); // 1 berarti fade in
        }
        // Jika player tidak dalam jangkauan dan tidak sedang fading, lakukan fade out
        else if (!inRange && !isFading)
        {
            StartCoroutine(FadeCanvas(0f)); // 0 berarti fade out
        }
    }

    private IEnumerator FadeCanvas(float targetAlpha)
    {
        isFading = true; // Tandai bahwa proses fading sedang berlangsung
        float startAlpha = canvasGroup.alpha;
        float time = 0;

        // Lakukan fade dari nilai alpha awal ke target alpha selama durasi yang ditentukan
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }

        // Set alpha akhir
        canvasGroup.alpha = targetAlpha;

        // Atur interaksi berdasarkan visibilitas canvas
        canvasGroup.interactable = targetAlpha == 1f;
        canvasGroup.blocksRaycasts = targetAlpha == 1f;

        isFading = false; // Tandai bahwa proses fading selesai
    }
}
