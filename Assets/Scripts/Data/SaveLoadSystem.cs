using System.IO;
using UnityEngine;

public class SaveLoadSettings : MonoBehaviour
{
    public float musicVolume = 0.5f; // Nilai default volume musik
    public float sfxVolume = 0.5f;   // Nilai default volume SFX
    private string settingsFile;

    private void Start()
    {
        settingsFile = Application.persistentDataPath + "/settings.json";
        LoadSettings();
    }

    // Simpan pengaturan ke file JSON
    public void SaveSettings()
    {
        GameSettings settings = new GameSettings();
        settings.musicVolume = musicVolume;
        settings.sfxVolume = sfxVolume; // Simpan volume SFX juga

        string json = JsonUtility.ToJson(settings);
        File.WriteAllText(settingsFile, json);

        Debug.Log("Settings Saved to JSON");
    }

    // Muat pengaturan dari file JSON
    public void LoadSettings()
    {
        if (File.Exists(settingsFile))
        {
            string json = File.ReadAllText(settingsFile);
            GameSettings settings = JsonUtility.FromJson<GameSettings>(json);
            musicVolume = settings.musicVolume;
            sfxVolume = settings.sfxVolume; // Muat volume SFX juga
            Debug.Log("Settings Loaded from JSON");
        }
        else
        {
            Debug.LogWarning("No settings file found, using default settings");
        }
    }
}
