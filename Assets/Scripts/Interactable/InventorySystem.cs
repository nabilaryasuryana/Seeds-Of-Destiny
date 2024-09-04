using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // List untuk menyimpan item dalam inventory
    public List<Item> items = new List<Item>();
    public int maxInventorySize = 20;

    // Method untuk menambahkan item ke dalam inventory
    public bool AddItem(Item item)
    {
        if (items.Count >= maxInventorySize)
        {
            Debug.Log("Inventory penuh!");
            return false;
        }
        items.Add(item);
        Debug.Log(item.itemName + " telah ditambahkan ke inventory.");
        return true;
    }

    // Method untuk menghapus item dari inventory
    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log(item.itemName + " telah dihapus dari inventory.");
        }
    }
}
