using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SaveData
{
    public List<string> completedDialogues = new List<string>(); // Menyimpan ID dialog yang sudah selesai
}

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }
    private string saveFilePath;
    private SaveData saveData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        saveFilePath = Application.persistentDataPath + "/saveData.json";
        LoadSaveData();
    }

    // Memuat data penyimpanan dari file JSON
    private void LoadSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            saveData = new SaveData();
        }
    }

    // Menyimpan data penyimpanan ke file JSON
    private void SaveToFile()
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, json);
    }

    // Menandai dialog sebagai selesai berdasarkan ID
    public void MarkDialogueAsCompleted(string dialogueID)
    {
        if (!saveData.completedDialogues.Contains(dialogueID))
        {
            saveData.completedDialogues.Add(dialogueID);
            SaveToFile();
        }
    }

    // Mengecek apakah dialog sudah selesai
    public bool IsDialogueCompleted(string dialogueID)
    {
        return saveData.completedDialogues.Contains(dialogueID);
    }
}
