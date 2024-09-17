using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DialogueState
{
    public string dialogueID;
    public int currentDialogueIndex;
    public bool dialoguePlayed;

    public DialogueState(string id, int index, bool played)
    {
        dialogueID = id;
        currentDialogueIndex = index;
        dialoguePlayed = played;
    }
}

[Serializable]
public class SaveData
{
    public List<DialogueState> dialogueStates = new List<DialogueState>(); // Menyimpan state dari dialog
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

    // Menyimpan status dialog ke dalam SaveData
    public void SaveDialogueState(string dialogueID, DialogueState newState)
    {
        bool found = false;
        for (int i = 0; i < saveData.dialogueStates.Count; i++)
        {
            if (saveData.dialogueStates[i].dialogueID == dialogueID)
            {
                saveData.dialogueStates[i] = newState; // Update state yang sudah ada
                found = true;
                break;
            }
        }
        if (!found)
        {
            saveData.dialogueStates.Add(newState); // Tambahkan state baru jika belum ada
        }
        SaveToFile(); // Simpan ke file setelah diperbarui
    }

    // Mengambil state dialog berdasarkan ID
    public DialogueState GetDialogueState(string dialogueID)
    {
        foreach (DialogueState state in saveData.dialogueStates)
        {
            if (state.dialogueID == dialogueID)
            {
                return state;
            }
        }
        return null; // Jika tidak ada state untuk ID tersebut
    }

    // Mengecek apakah dialog sudah selesai
    public bool IsDialogueCompleted(string dialogueID)
    {
        DialogueState state = GetDialogueState(dialogueID);
        return state != null && state.dialoguePlayed;
    }

    // Menandai dialog sebagai selesai berdasarkan ID
    public void MarkDialogueAsCompleted(string dialogueID, int currentDialogueIndex)
    {
        DialogueState state = new DialogueState(dialogueID, currentDialogueIndex, true);
        SaveDialogueState(dialogueID, state);
    }
}
