using System.Collections;
using UnityEngine;

public class ItemDropAnimator : MonoBehaviour
{
    [SerializeField] private float popDuration = 1f; // Durasi animasi pop
    [SerializeField] private float heightY = 2f; // Ketinggian maksimum animasi pop
    [SerializeField] private AnimationCurve animCurve; // Kurva animasi untuk ketinggian gerakan

    // Fungsi untuk menjalankan animasi drop
    public IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f); // Posisi X acak untuk variasi
        float randomY = transform.position.y + Random.Range(-1f, 1f); // Posisi Y acak untuk variasi

        Vector2 endPoint = new Vector2(randomX, randomY); // Titik akhir animasi
        float timePassed = 0f;

        // Loop animasi selama durasi yang ditentukan
        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;

            // Evaluasi tinggi berdasarkan kurva animasi
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            // Update posisi dengan interpolasi dan tinggi kurva
            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);

            // Tambahkan sedikit rotasi untuk efek lebih menarik
            transform.Rotate(0f, 0f, 360f * Time.deltaTime * heightT);

            yield return null;
        }

        // Drop ke posisi akhir dengan animasi jatuh
        yield return FallDownEffect(endPoint);
    }

    // Efek jatuh akhir dengan sedikit bounce
    private IEnumerator FallDownEffect(Vector2 endPoint)
    {
        float fallDuration = 0.3f; // Durasi jatuh
        float bounceHeight = 0.5f; // Tinggi bounce setelah jatuh
        float fallTimePassed = 0f;

        Vector2 bouncePoint = endPoint + new Vector2(0f, bounceHeight);

        // Naik sedikit sebelum jatuh
        while (fallTimePassed < fallDuration)
        {
            fallTimePassed += Time.deltaTime;
            float linearT = fallTimePassed / fallDuration;

            // Gerakan ke atas dan jatuh menggunakan kurva animasi
            transform.position = Vector2.Lerp(bouncePoint, endPoint, animCurve.Evaluate(linearT));
            yield return null;
        }
    }
}
