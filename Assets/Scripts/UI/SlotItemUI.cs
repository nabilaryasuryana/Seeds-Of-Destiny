using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotItemUI : MonoBehaviour
{
    public Image itemIcon;
    public TMP_Text itemName;

    // Method untuk mengatur tampilan item di slot
    public void SetItem(Item item)
    {
        if (item != null)
        {
            itemIcon.sprite = item.itemIcon;
            itemName.text = item.itemName;
            itemIcon.enabled = true; // Aktifkan ikon jika ada item
            itemName.enabled = true; // Aktifkan nama jika ada item
        }
        else
        {
            itemIcon.enabled = false; // Nonaktifkan ikon jika tidak ada item
            itemName.enabled = false; // Nonaktifkan nama jika tidak ada item
        }
    }
}
