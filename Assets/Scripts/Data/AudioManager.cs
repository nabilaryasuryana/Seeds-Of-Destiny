using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource; // Objek AudioSource untuk musik
    public AudioSource sfxSource;   // Objek AudioSource untuk SFX
    public SaveLoadSettings saveLoadSettings;

    private void Start()
    {
        UpdateAudioVolumes();
    }

    // Mengupdate volume musik dan SFX berdasarkan pengaturan
    public void UpdateAudioVolumes()
    {
        if (musicSource != null && saveLoadSettings != null)
        {
            musicSource.volume = saveLoadSettings.musicVolume;
        }

        if (sfxSource != null && saveLoadSettings != null)
        {
            sfxSource.volume = saveLoadSettings.sfxVolume;
        }
    }

    // Mengubah volume musik dari slider (UI)
    public void SetMusicVolume(float volume)
    {
        if (saveLoadSettings != null)
        {
            saveLoadSettings.musicVolume = volume;
            saveLoadSettings.SaveSettings();
            UpdateAudioVolumes();
        }
    }

    // Mengubah volume SFX dari slider (UI)
    public void SetSFXVolume(float volume)
    {
        if (saveLoadSettings != null)
        {
            saveLoadSettings.sfxVolume = volume;
            saveLoadSettings.SaveSettings();
            UpdateAudioVolumes();
        }
    }
}
