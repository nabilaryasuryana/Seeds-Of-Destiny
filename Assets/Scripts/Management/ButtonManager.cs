using UnityEngine;
using UnityEngine.SceneManagement; // Untuk perpindahan scene
using UnityEngine.Audio; // Untuk pengaturan audio

public class ButtonManager : MonoBehaviour
{
    // Fungsi untuk keluar dari aplikasi
    public void QuitGame()
    {
        Debug.Log("Keluar dari permainan.");
        Application.Quit();
    }
}
