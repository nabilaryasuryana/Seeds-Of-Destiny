using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public Slider musicVolumeSlider; // Slider untuk volume musik
    public Slider sfxVolumeSlider;   // Slider untuk volume SFX
    public AudioManager audioManager; // Referensi ke AudioManager

    private void Start()
    {
        // Cek jika audioManager dan slider tidak null sebelum mengakses
        if (audioManager != null)
        {
            // Setel slider musik ke nilai yang disimpan
            if (musicVolumeSlider != null)
                musicVolumeSlider.value = audioManager.saveLoadSettings.musicVolume;
            
            if (sfxVolumeSlider != null)
                sfxVolumeSlider.value = audioManager.saveLoadSettings.sfxVolume;
        }

        // Listener untuk perubahan volume musik
        if (musicVolumeSlider != null)
            musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChanged(); });

        // Listener untuk perubahan volume SFX
        if (sfxVolumeSlider != null)
            sfxVolumeSlider.onValueChanged.AddListener(delegate { OnSFXVolumeChanged(); });

        // Sembunyikan panel setting (jika ingin menyembunyikan GameObject yang menjalankan script ini)
        gameObject.SetActive(false);
    }

    // Dipanggil saat slider volume musik berubah
    public void OnMusicVolumeChanged()
    {
        if (audioManager != null)
        {
            audioManager.SetMusicVolume(musicVolumeSlider.value);
        }
    }

    // Dipanggil saat slider volume SFX berubah
    public void OnSFXVolumeChanged()
    {
        if (audioManager != null)
        {
            audioManager.SetSFXVolume(sfxVolumeSlider.value);
        }
    }
}
