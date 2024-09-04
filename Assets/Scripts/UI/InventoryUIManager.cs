using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventoryPanel; // Panel tempat slot item berada
    public GameObject slotPrefab; // Prefab slot item
    public InventorySystem playerInventory; // Referensi ke InventorySystem milik Player
    
    private List<GameObject> slots = new List<GameObject>(); // List untuk menyimpan slot item

    void Start()
    {
        UpdateInventoryUI();
    }

    // Method untuk memperbarui tampilan UI inventory
    public void UpdateInventoryUI()
    {
        // Bersihkan slot yang lama
        foreach (GameObject slot in slots)
        {
            Destroy(slot);
        }
        slots.Clear();

        // Tambahkan slot baru untuk setiap item di inventory
        foreach (Item item in playerInventory.items)
        {
            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel.transform);
            SlotItemUI slotUI = newSlot.GetComponent<SlotItemUI>();
            slotUI.SetItem(item);
            slots.Add(newSlot);
        }
    }
}
