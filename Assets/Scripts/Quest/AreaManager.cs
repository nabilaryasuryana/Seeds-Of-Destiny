using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance; // Singleton Instance untuk diakses oleh QuestClearArea
    public List<int> areaIDs; // Daftar area ID yang ada di scene
    public List<int> clearedAreas; // Daftar area ID yang sudah dibersihkan

    private string saveFilePath;

    private void Awake()
    {
        // Inisialisasi singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Tentukan path tempat penyimpanan file JSON
        saveFilePath = Path.Combine(Application.persistentDataPath, "areaProgress.json");

        // Inisialisasi clearedAreas jika belum ada
        if (clearedAreas == null)
        {
            clearedAreas = new List<int>();
        }

        // Load progress dari file JSON saat game dimulai
        LoadProgress();
    }

    // Fungsi untuk memeriksa apakah area sudah dibersihkan berdasarkan ID
    public bool IsAreaCleared(int areaID)
    {
        return clearedAreas.Contains(areaID);
    }

    // Fungsi untuk menandai area sebagai bersih berdasarkan ID
    public void SetAreaCleared(int areaID)
    {
        if (!clearedAreas.Contains(areaID))
        {
            clearedAreas.Add(areaID);
            Debug.Log($"Area {areaID} telah ditandai sebagai bersih.");

            // Simpan progres setelah area ditandai sebagai bersih
            SaveProgress();
        }
    }

    // Fungsi untuk menyimpan progres ke file JSON
    public void SaveProgress()
    {
        AreaProgressData progressData = new AreaProgressData();
        progressData.clearedAreas = clearedAreas;

        // Konversi data ke JSON
        string json = JsonUtility.ToJson(progressData, true);

        // Simpan JSON ke file
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Progres area telah disimpan ke " + saveFilePath);
    }

    // Fungsi untuk memuat progres dari file JSON
    public void LoadProgress()
    {
        if (File.Exists(saveFilePath))
        {
            // Baca JSON dari file
            string json = File.ReadAllText(saveFilePath);

            // Konversi kembali JSON menjadi objek
            AreaProgressData progressData = JsonUtility.FromJson<AreaProgressData>(json);

            // Muat kembali data clearedAreas
            if (progressData != null)
            {
                clearedAreas = progressData.clearedAreas;
                Debug.Log("Progres area telah dimuat dari " + saveFilePath);
            }
        }
        else
        {
            Debug.Log("Tidak ada progres area yang ditemukan, membuat data baru.");
        }
    }
}

// Kelas untuk menyimpan data progres area
[System.Serializable]
public class AreaProgressData
{
    public List<int> clearedAreas; // Daftar area ID yang sudah dibersihkan
}
