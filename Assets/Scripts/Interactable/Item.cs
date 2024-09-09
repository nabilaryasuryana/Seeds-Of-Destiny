using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // Nama item
    public Sprite itemIcon; // Ikon yang akan ditampilkan di UI
    public string description; // Deskripsi item
    public GameObject itemPrefab; // Prefab dari item yang bisa dijatuhkan atau digunakan
    public bool destroyable;

    public virtual void Use()
    {
        Debug.Log("Using base item: " + itemName);
        
        // Tentukan apakah item ini akan dihancurkan setelah digunakan
        if (destroyable)
        {
            DestroyItem();
        }
    }

    protected void DestroyItem()
    {
        Debug.Log($"Destroying item: {itemName}");
        // Tambahkan logika untuk menghapus item dari inventory, misal:
        // InventorySystem.Instance.RemoveItem(this);
    }
}
