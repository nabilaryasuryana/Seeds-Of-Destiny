using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public Slider musicVolumeSlider; // Slider untuk volume musik
    public Slider sfxVolumeSlider;   // Slider untuk volume SFX
    public AudioManager audioManager;

    private void Start()
    {
        // Setel slider musik ke nilai yang disimpan
        if (audioManager != null)
        {
            musicVolumeSlider.value = audioManager.saveLoadSettings.musicVolume;
            sfxVolumeSlider.value = audioManager.saveLoadSettings.sfxVolume;
        }

        // Listener untuk perubahan volume musik
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChanged(); });
        // Listener untuk perubahan volume SFX
        sfxVolumeSlider.onValueChanged.AddListener(delegate { OnSFXVolumeChanged(); });
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
