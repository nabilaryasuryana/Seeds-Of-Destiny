using System.Collections.Generic;
using UnityEngine;

public class BossDropRate : MonoBehaviour
{
    [System.Serializable]
    public class DropItem
    {
        public GameObject itemPrefab; // Prefab item yang akan dijatuhkan
        public int minDropAmount = 1; // Jumlah minimum yang dapat dijatuhkan
        public int maxDropAmount = 1; // Jumlah maksimum yang dapat dijatuhkan
        public float dropRate = 1f; // Probabilitas drop untuk item random (0 - 1)
        public bool isGuaranteed = false; // Apakah item ini pasti dijatuhkan
    }

    [SerializeField] private List<DropItem> dropItems; // Daftar semua item drop
    [SerializeField] private int maxTotalDrops = 5; // Jumlah total item drop maksimal

    private List<GameObject> itemsToDrop = new List<GameObject>(); // Daftar item yang akan dijatuhkan

    // Fungsi ini harus dipanggil ketika bos dikalahkan
    public void DropItems()
    {
        List<GameObject> guaranteedItems = new List<GameObject>();
        List<GameObject> randomItems = new List<GameObject>();

        // Pisahkan item pasti dan item random
        foreach (var dropItem in dropItems)
        {
            int dropAmount = Random.Range(dropItem.minDropAmount, dropItem.maxDropAmount + 1);

            // Tambahkan item pasti ke daftar
            if (dropItem.isGuaranteed)
            {
                for (int i = 0; i < dropAmount; i++)
                {
                    guaranteedItems.Add(dropItem.itemPrefab);
                }
            }
            else
            {
                // Tambahkan item random berdasarkan droprate
                for (int i = 0; i < dropAmount; i++)
                {
                    if (Random.value <= dropItem.dropRate)
                    {
                        randomItems.Add(dropItem.itemPrefab);
                    }
                }
            }
        }

        // Tambahkan item pasti terlebih dahulu
        itemsToDrop.AddRange(guaranteedItems);

        // Tambahkan item random jika masih di bawah maxTotalDrops
        foreach (var item in randomItems)
        {
            if (itemsToDrop.Count < maxTotalDrops)
            {
                itemsToDrop.Add(item);
            }
            else
            {
                break; // Jika jumlah item sudah mencapai batas maksimum, berhenti menambahkan
            }
        }

        // Drop semua item yang sudah terpilih
        foreach (var item in itemsToDrop)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        // Bersihkan list setelah menjatuhkan item
        itemsToDrop.Clear();
    }
}
