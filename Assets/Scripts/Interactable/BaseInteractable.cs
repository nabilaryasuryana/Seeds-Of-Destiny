using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable : MonoBehaviour
{
    [Header("Base Interactable Config")]
    public CircleCollider2D triggerArea; // Trigger area yang digunakan untuk mendeteksi interaksi
    public GameObject canvas; // UI canvas untuk menampilkan instruksi atau informasi

    protected bool isPlayerNearby; // Menyimpan status apakah player berada di sekitar
    
    protected virtual void Awake()
    {
        isPlayerNearby = false;
        if (canvas != null)
        {
            canvas.SetActive(false); // Sembunyikan canvas di awal
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // Cek jika player memasuki area trigger
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            OnPlayerEnter(other); // Panggil event khusus ketika player masuk
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        // Cek jika player meninggalkan area trigger
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            OnPlayerExit(other); // Panggil event khusus ketika player keluar
        }
    }

    protected virtual void Update()
    {
        // Tampilkan canvas jika player berada di sekitar
        if (canvas != null)
        {
            canvas.SetActive(isPlayerNearby);
        }

        // Lakukan interaksi jika kondisi dipenuhi
        if (isPlayerNearby)
        {
            //HandleInteraction();
        }
    }

    // Method untuk menangani interaksi, dapat di-override oleh turunan
    protected virtual void HandleInteraction() 
    {
        // Kosong, diisi oleh turunan
    }

    // Method dipanggil ketika player masuk, bisa di-override jika diperlukan
    protected virtual void OnPlayerEnter(Collider2D player) 
    {
        Debug.Log("Player has entered interaction area.");
    }

    // Method dipanggil ketika player keluar, bisa di-override jika diperlukan
    protected virtual void OnPlayerExit(Collider2D player) 
    {
        Debug.Log("Player has exited interaction area.");
    }
}
