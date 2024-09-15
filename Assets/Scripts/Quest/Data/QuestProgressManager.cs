using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class QuestProgressManager
{
    private static string filePath = Application.persistentDataPath + "/questProgress.json";

    public static void SaveProgress(List<Quest> activeQuests, List<Quest> completedQuests)
    {
        QuestProgressData questData = new QuestProgressData
        {
            activeQuests = activeQuests,
            completedQuests = completedQuests
        };

        // Serialisasi ke JSON
        string json = JsonUtility.ToJson(questData, true);

        // Simpan ke file
        File.WriteAllText(filePath, json);
        Debug.Log("Quest progress saved to " + filePath);
    }

    public static QuestProgressData LoadProgress()
    {
        if (File.Exists(filePath))
        {
            // Membaca file JSON dan deserialisasi
            string json = File.ReadAllText(filePath);
            QuestProgressData questData = JsonUtility.FromJson<QuestProgressData>(json);
            Debug.Log("Quest progress loaded from " + filePath);
            return questData;
        }
        else
        {
            Debug.LogWarning("No quest progress file found.");
            return null;
        }
    }
}
