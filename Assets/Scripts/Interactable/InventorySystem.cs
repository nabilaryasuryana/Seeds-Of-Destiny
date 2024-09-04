using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<Item> items = new List<Item>(); // Daftar item dalam inventory
    public Transform playerTransform; // Transformasi player untuk menentukan posisi drop item
    public int maxInventorySize = 20;
    private Item selectedItem; // Item yang sedang dipilih

    public bool AddItem(Item item)
    {
        if (items.Count < maxInventorySize) // Maksimal 20 item
        {
            items.Add(item);
            return true;
        }
        return false;
    }

    public void DropSelectedItem()
    {
        if (selectedItem != null && items.Contains(selectedItem))
        {
            items.Remove(selectedItem);

            // Cek apakah item memiliki prefab untuk dijatuhkan
            if (selectedItem.itemPrefab != null)
            {
                // Instantiate prefab di posisi dekat player
                Vector3 dropPosition = playerTransform.position + playerTransform.forward;
                Instantiate(selectedItem.itemPrefab, dropPosition, Quaternion.identity);
            }
            selectedItem = null; // Reset setelah drop
        }
    }

    public void SelectItem(Item item)
    {
        selectedItem = item; // Set item yang dipilih
    }

    public void UseSelectedItem()
    {
        if (selectedItem != null)
        {
            // Tambahkan logika penggunaan item (sesuaikan dengan kebutuhan game)
            Debug.Log("Using item: " + selectedItem.itemName);
            selectedItem = null; // Reset setelah penggunaan
        }
    }

    public void MoveItem(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || toIndex < 0 || fromIndex >= items.Count || toIndex >= items.Count)
        {
            Debug.LogWarning("Invalid index for moving item");
            return;
        }

        // Tukar item di antara dua slot
        Item temp = items[toIndex];
        items[toIndex] = items[fromIndex];
        items[fromIndex] = temp;
    }
}
