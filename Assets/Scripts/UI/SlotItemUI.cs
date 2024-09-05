using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotItemUI : MonoBehaviour
{
    public Image itemIcon; // Icon item
    public TMP_Text itemName; // Nama item
    public Button useButton; // Tombol untuk menggunakan item
    public Button dropButton; // Tombol untuk drop item
    public Button selectButton; // Tombol untuk memilih slot
    public GameObject itemMenu; // Menu untuk item yang sedang dipilih
    public GameObject placeholder; // Placeholder UI yang menunjukkan slot kosong

    private Item currentItem; // Item yang ditampilkan di slot ini
    private InventorySystem playerInventory; // Referensi ke inventory pemain
    private InventoryUIManager inventoryUIManager; // Referensi ke UI manager inventory
    private int slotIndex; // Index slot di inventory

    public void SetItem(Item item, InventorySystem inventory, InventoryUIManager uiManager, int index)
    {
        currentItem = item;
        playerInventory = inventory;
        inventoryUIManager = uiManager;
        slotIndex = index;

        // Pastikan semua komponen terhubung
        if (itemIcon == null || itemName == null || useButton == null || dropButton == null || selectButton == null || placeholder == null)
        {
            Debug.LogError("Referensi UI komponen tidak terhubung dengan benar di SlotItemUI.");
            return;
        }

        if (item != null)
        {
            // Set UI untuk item
            itemIcon.sprite = item.itemIcon;
            itemName.text = item.itemName;
            itemIcon.enabled = true;
            itemName.enabled = true;
            useButton.gameObject.SetActive(true);
            dropButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(true);
            placeholder.SetActive(false); // Nonaktifkan placeholder saat ada item

            // Set up listeners
            useButton.onClick.RemoveAllListeners();
            useButton.onClick.AddListener(OnUseButtonClicked);

            dropButton.onClick.RemoveAllListeners();
            dropButton.onClick.AddListener(OnDropButtonClicked);

            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(OnSelectButtonClicked);
        }
        else
        {
            // Tampilkan placeholder saat slot kosong
            itemIcon.enabled = false;
            itemName.enabled = false;
            useButton.gameObject.SetActive(false);
            dropButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(false);
            placeholder.SetActive(true); // Aktifkan placeholder saat tidak ada item
        }
    }

    void Awake()
    {
        itemMenu.SetActive(false);
    }

    // Metode untuk saat tombol select ditekan
    public void OnSelectButtonClicked()
    {
        inventoryUIManager.SetSelectedSlot(this);
        playerInventory.SelectItem(currentItem);
    }

    // Metode untuk saat tombol use ditekan
    public void OnUseButtonClicked()
    {
        inventoryUIManager.UseSelectedItem();
    }

    // Metode untuk saat tombol drop ditekan
    public void OnDropButtonClicked()
    {
        inventoryUIManager.DropSelectedItem();
    }

    // Metode untuk menandai slot sebagai terpilih
    public void Select()
    {
        itemMenu.SetActive(true);
        GetComponent<Image>().color = Color.green; // Warna hijau saat dipilih
    }

    // Metode untuk menghapus tanda terpilih
    public void Deselect()
    {
        itemMenu.SetActive(false);
        GetComponent<Image>().color = Color.white; // Reset ke warna awal
    }
}
