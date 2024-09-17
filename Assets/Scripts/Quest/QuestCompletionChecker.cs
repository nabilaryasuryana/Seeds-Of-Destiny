using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Reflection;

[System.Serializable]
public class QuestCompletionData
{
    public int objectiveId;
    public bool hasExecuted;
}

public class QuestCompletionChecker : MonoBehaviour
{
    public int objectiveId; // ID dari objective yang ingin diperiksa
    public MonoBehaviour scriptToExecute; // Script yang akan dijalankan saat quest selesai
    public string methodName; // Nama metode di script yang akan dijalankan

    private string dataFilePath;

    private void Start()
    {
        // Set path untuk file JSON
        dataFilePath = Path.Combine(Application.persistentDataPath, "questCompletionData.json");

        // Periksa status quest dan file JSON
        if (HasExecuted())
        {
            Destroy(this); // Hancurkan script jika sudah dieksekusi
        }
        else
        {
            CheckQuestCompletion();
        }
    }

    private void Update()
    {
        if (HasExecuted())
        {
            Destroy(this); // Hancurkan script jika sudah dieksekusi
        }
        else
        {
            CheckQuestCompletion();
        }
    }

    public void CheckQuestCompletion()
    {
        // Dapatkan quest yang sesuai dengan objectiveId dari QuestLog
        List<Quest> activeQuests = QuestLog.GetActiveQuests();
        List<Quest> completedQuests = QuestLog.GetCompletedQuests();

        // Periksa di questList (aktif)
        foreach (Quest quest in activeQuests)
        {
            if (quest.objective.objectiveId == objectiveId)
            {
                if (quest.objective.currentAmount >= quest.objective.amount)
                {
                    // Jika quest telah selesai, jalankan metode pada script yang ditentukan
                    ExecuteMethod();
                    SaveExecutionStatus();
                }
                return; // Keluar dari loop setelah menemukan quest yang sesuai
            }
        }

        // Jika tidak ditemukan di questList, periksa di completedQuest
        foreach (Quest quest in completedQuests)
        {
            if (quest.objective.objectiveId == objectiveId)
            {
                if (quest.objective.currentAmount >= quest.objective.amount)
                {
                    // Jika quest telah selesai, jalankan metode pada script yang ditentukan
                    ExecuteMethod();
                    SaveExecutionStatus();
                }
                return; // Keluar dari loop setelah menemukan quest yang sesuai
            }
        }
    }

    private void ExecuteMethod()
    {
        if (scriptToExecute != null)
        {
            // Gunakan reflection untuk memanggil metode berdasarkan nama metode
            var method = scriptToExecute.GetType().GetMethod(methodName);
            if (method != null)
            {
                method.Invoke(scriptToExecute, null);
            }
            else
            {
                Debug.LogWarning("Method " + methodName + " not found in script " + scriptToExecute.GetType().Name);
            }
        }
        else
        {
            Debug.LogWarning("Script to execute is not assigned.");
        }
    }

    private void SaveExecutionStatus()
    {
        // Buat data untuk disimpan
        QuestCompletionData data = new QuestCompletionData
        {
            objectiveId = objectiveId,
            hasExecuted = true
        };

        // Konversi data ke JSON
        string json = JsonUtility.ToJson(data);

        // Simpan JSON ke file
        File.WriteAllText(dataFilePath, json);

        Debug.Log("Quest completion status saved to " + dataFilePath);
    }

    private bool HasExecuted()
    {
        if (File.Exists(dataFilePath))
        {
            // Baca file JSON
            string json = File.ReadAllText(dataFilePath);

            // Parse JSON ke objek QuestCompletionData
            QuestCompletionData data = JsonUtility.FromJson<QuestCompletionData>(json);

            // Periksa apakah script ini sudah dijalankan
            return data.objectiveId == objectiveId && data.hasExecuted;
        }
        return false;
    }
}
