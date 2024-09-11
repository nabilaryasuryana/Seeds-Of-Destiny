using System.Collections;
using UnityEngine;

public class ChargeAttack : MonoBehaviour, IEnemy
{
    [SerializeField] private float initialChargeSpeed = 5f; // Kecepatan awal charge
    [SerializeField] private float maxChargeSpeed = 15f; // Kecepatan maksimum charge
    [SerializeField] private float maxChargeDamage = 5f; // Damage maksimum yang bisa diberikan
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer trailRenderer; // Referensi untuk Trail Renderer
    [SerializeField] private float chargeDuration = 5f; // Durasi charge

    private bool isCharging = false; // Status apakah bos sedang charging
    private Vector2 chargeTargetPosition; // Posisi target saat charge dimulai
    private float currentChargeSpeed; // Kecepatan saat ini

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (trailRenderer != null)
        {
            trailRenderer.emitting = false; // Mulai dengan Trail Renderer nonaktif
        }
    }

    // Fungsi untuk memulai serangan
    public void Attack()
    {
        StartCoroutine(ChargeTowardsLastPlayerPosition());
    }

    // Coroutine untuk melakukan charge ke arah posisi terakhir player
    private IEnumerator ChargeTowardsLastPlayerPosition()
    {
        isCharging = true;
        currentChargeSpeed = initialChargeSpeed; // Set kecepatan awal
        chargeTargetPosition = PlayerController.Instance.transform.position; // Ambil posisi terakhir player saat charge dimulai

        if (trailRenderer != null)
        {
            trailRenderer.emitting = true; // Aktifkan Trail Renderer saat charging
        }

        float chargeTime = 0f;

        while (chargeTime < chargeDuration)
        {
            // Tingkatkan kecepatan seiring waktu hingga batas maksimal
            currentChargeSpeed = Mathf.Lerp(initialChargeSpeed, maxChargeSpeed, chargeTime / chargeDuration);
            Vector2 direction = (chargeTargetPosition - (Vector2)transform.position).normalized; // Arah menuju posisi terakhir player
            rb.velocity = direction * currentChargeSpeed; // Gerakkan bos ke arah target

            chargeTime += Time.deltaTime;
            yield return null; // Tunggu frame berikutnya
        }

        // Hentikan gerakan setelah charge selesai
        rb.velocity = Vector2.zero;
        isCharging = false;

        if (trailRenderer != null)
        {
            trailRenderer.emitting = false; // Nonaktifkan Trail Renderer setelah charging selesai
        }
    }

    // Deteksi tabrakan dengan player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek apakah bos sedang charging
        if (!isCharging) return;

        // Cek apakah bos bertabrakan dengan objek yang memiliki tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Hentikan charging setelah bertabrakan dengan player
            rb.velocity = Vector2.zero;
            isCharging = false;

            if (trailRenderer != null)
            {
                trailRenderer.emitting = false; // Nonaktifkan Trail Renderer setelah tabrakan
            }

            // Hitung damage berdasarkan kecepatan saat tabrakan
            float currentSpeed = collision.relativeVelocity.magnitude; // Ambil kecepatan saat tabrakan
            float calculatedDamage = Mathf.Clamp(currentSpeed / maxChargeSpeed * maxChargeDamage, 1, maxChargeDamage);

            // Convert damage menjadi integer dan kirim ke PlayerHealth
            PlayerHealth.Instance.TakeDamage(Mathf.CeilToInt(calculatedDamage), transform);
        }
        else
        {
            // Jika bertabrakan dengan selain player (misalnya tembok), hentikan charge tanpa memberikan damage
            rb.velocity = Vector2.zero;
            isCharging = false;

            if (trailRenderer != null)
            {
                trailRenderer.emitting = false; // Nonaktifkan Trail Renderer setelah tabrakan
            }
        }
    }
}
