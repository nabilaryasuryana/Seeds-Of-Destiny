using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestClearedArea : MonoBehaviour
{
    public int areaID = 1; // ID dari tujuan untuk quest 'goTo'
    public int objectiveId  = 1;

    private bool questCompleted = false; // Untuk memastikan quest hanya dipanggil sekali
    private string saveFilePath;

    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "/questData.json";
        LoadQuestData(); // Muat data quest saat mulai
    }

    void Update()
    {
        // Cek jika belum menyelesaikan quest dan tidak ada objek dengan tag 'Enemy'
        if (!questCompleted && !HasEnemiesInScene())
        {
            // Panggil QuestLog untuk mengecek quest 'goTo'
            QuestLog.CheckQuestObjective(Quest.Objective.Type.ClearArea, objectiveId);
            questCompleted = true; // Tandai quest sudah selesai
            SaveQuestData(); // Simpan data quest setelah menyelesaikan
        }
    }

    // Fungsi untuk memeriksa keberadaan objek dengan tag 'Enemy' dalam scene
    private bool HasEnemiesInScene()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length > 0;
    }

    // Fungsi untuk menyimpan data quest ke file JSON
    private void SaveQuestData()
    {
        QuestData data = new QuestData
        {
            questCompleted = questCompleted,
            areaID = areaID
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Quest data saved to " + saveFilePath);
    }

    // Fungsi untuk memuat data quest dari file JSON
    private void LoadQuestData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            QuestData data = JsonUtility.FromJson<QuestData>(json);

            questCompleted = data.questCompleted;
            areaID = data.areaID;

            Debug.Log("Quest data loaded from " + saveFilePath);
        }
        else
        {
            Debug.Log("No quest data found. Starting with default settings.");
        }
    }
}

[Serializable]
public class QuestData
{
    public bool questCompleted;
    public int areaID;
}