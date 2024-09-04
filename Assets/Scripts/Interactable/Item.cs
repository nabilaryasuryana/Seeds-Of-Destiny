using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // Nama item
    public Sprite itemIcon; // Ikon yang akan ditampilkan di UI
    public string description; // Deskripsi item
    public GameObject itemPrefab; // Prefab dari item yang bisa dijatuhkan atau digunakan
}
