using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestClearArea : MonoBehaviour
{
    public int areaID; // ID dari area yang harus di-clear
    public int objectiveId;
    private AreaManager areaManager; // Referensi ke AreaManager untuk memeriksa status area

    private void Start()
    {
        // Mendapatkan referensi ke AreaManager di scene
        areaManager = AreaManager.Instance;

        // Jika area sudah bersih, hancurkan script ini
        if (areaManager.IsAreaCleared(areaID))
        {
            Debug.Log($"Area {areaID} sudah bersih, script QuestClearArea dihentikan.");
            Destroy(this); // Script dihapus karena area sudah bersih
        }
    }

    private void Update()
    {
        // Jika area belum bersih dan semua musuh sudah hilang, tandai area sebagai bersih
        if (!areaManager.IsAreaCleared(areaID) && !HasEnemiesInScene())
        {
            Debug.Log($"Area {areaID} berhasil dibersihkan.");
            QuestLog.CheckQuestObjective(Quest.Objective.Type.ClearArea, objectiveId);
            areaManager.SetAreaCleared(areaID); // Tandai area sebagai bersih
            Destroy(this); // Hancurkan script karena area sudah bersih
        }
    }

    // Fungsi untuk memeriksa apakah masih ada musuh di dalam scene
    private bool HasEnemiesInScene()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length > 0;
    }
}
