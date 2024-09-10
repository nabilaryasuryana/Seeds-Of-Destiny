using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // Singleton instance
    public static InventorySystem Instance { get; private set; }

    public List<Item> items = new List<Item>(); // Daftar item dalam inventory
    public Transform playerTransform; // Transformasi player untuk menentukan posisi drop item
    public int maxInventorySize = 20; // Maksimal ukuran inventory
    private Item selectedItem; // Item yang sedang dipilih

    private void Awake()
    {
        // Implementasi Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Hancurkan instance lain jika sudah ada
            return;
        }
        Instance = this;
    }

    // Menambahkan item ke dalam inventory
    public bool AddItem(Item item)
    {
        if (items.Count < maxInventorySize) // Cek jika masih ada ruang di inventory
        {
            items.Add(item);
            return true;
        }
        return false;
    }

    // Menggunakan item yang dipilih
    public void UseSelectedItem()
    {
        if (selectedItem != null)
        {
            selectedItem.Use(); // Panggil metode Use() pada item

            // Jika item destroyable, hapus dari inventory
            if (selectedItem.destroyable && items.Contains(selectedItem))
            {
                items.Remove(selectedItem);
                Debug.Log($"{selectedItem.itemName} has been used and destroyed.");
            }

            selectedItem = null; // Reset setelah penggunaan
        }
    }

    // Menghapus (drop) item yang dipilih dari inventory dan meletakkannya di dunia game
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

    // Memilih item untuk digunakan atau dipindahkan
    public void SelectItem(Item item)
    {
        selectedItem = item; // Set item yang dipilih
    }

    // Memindahkan item dari satu slot ke slot lain
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
