using UnityEngine;
using UnityEngine.UI;

public class BossHPDisplay : MonoBehaviour
{
    // [Header("Boss HP Settings")]
    // public Slider hpSlider;           // Referensi ke slider HP
    // public EnemyHealth bossHealth;    // Referensi ke script yang mengelola HP boss
    // public Transform player;          // Referensi ke pemain
    // public Transform boss;            // Referensi ke boss

    // [Header("Activation Settings")]
    // public float activationRadius = 20f; // Radius di mana slider akan diaktifkan

    // private bool isPlayerInRange = false; // Menyimpan status apakah player dalam jangkauan

    // private void Start()
    // {
    //     // Cari objek Player jika belum di-assign
    //     if (player == null)
    //     {
    //         GameObject playerObject = GameObject.FindWithTag("Player");
    //         if (playerObject != null)
    //         {
    //             player = playerObject.transform;
    //         }
    //         else
    //         {
    //             Debug.LogError("Player object with tag 'Player' not found!");
    //         }
    //     }

    //     // Cari objek Boss jika belum di-assign
    //     if (boss == null)
    //     {
    //         GameObject bossObject = GameObject.FindWithTag("Boss");
    //         if (bossObject != null)
    //         {
    //             boss = bossObject.transform;
    //         }
    //         else
    //         {
    //             Debug.LogError("Boss object with tag 'Boss' not found!");
    //             return; // Exit start jika boss tidak ditemukan
    //         }
    //     }

    //     // Pastikan slider mulai dalam kondisi tidak aktif
    //     hpSlider.gameObject.SetActive(false);

    //     // Inisialisasi nilai slider sesuai dengan HP boss saat ini
    //     if (bossHealth != null)
    //     {
    //         hpSlider.maxValue = bossHealth.StartingHealth(); // Pastikan method sesuai dengan class EnemyHealth
    //         hpSlider.value = bossHealth.CurrentHealth();
    //     }
    // }

    // private void Update()
    // {
    //     // Cek jarak antara pemain dan boss
    //     float distanceToPlayer = Vector3.Distance(player.position, boss.position);

    //     // Aktifkan slider jika pemain dalam radius jangkauan
    //     if (distanceToPlayer <= activationRadius)
    //     {
    //         if (!isPlayerInRange)
    //         {
    //             hpSlider.gameObject.SetActive(true);
    //             isPlayerInRange = true;
    //         }
    //     }
    //     else
    //     {
    //         if (isPlayerInRange)
    //         {
    //             hpSlider.gameObject.SetActive(false);
    //             isPlayerInRange = false;
    //         }
    //     }

    //     // Update nilai slider sesuai dengan HP boss
    //     if (bossHealth != null)
    //     {
    //         hpSlider.value = bossHealth.CurrentHealth();
    //     }
    // }
}
