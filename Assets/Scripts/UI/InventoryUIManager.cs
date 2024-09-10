using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InventoryUIManager : MonoBehaviour
{
    [Header("Configuration")]
    public GameObject inventoryPanel; // Panel tempat slot item berada
    public GameObject slotPrefab; // Prefab slot item
    public GameObject emptySlotPrefab; // Prefab slot kosong (placeholder)
    public InventorySystem playerInventory; // Referensi ke InventorySystem milik Player
    public int maxSlots = 20; // Maksimal slot inventory
    public InputAction openButton; // Tombol untuk membuka/tutup inventory
    public GameObject inventoryContainer; // Kontainer yang menampung seluruh UI inventory

    private List<SlotItemUI> slots = new List<SlotItemUI>(); // List untuk menyimpan slot item
    private bool toggleInventory; // Status buka/tutup inventory
    private PausePanel pausePanel; // Referensi ke panel pause (jika ada)
    private SlotItemUI selectedSlot; // Slot yang sedang dipilih oleh pemain

    void Start()
    {
        playerInventory = FindObjectOfType<InventorySystem>(); // Pindahkan ini sebelum UpdateInventoryUI
        if (playerInventory == null)
        {
            Debug.LogError("InventorySystem not found!");
            return; // Mencegah UpdateInventoryUI jika playerInventory tidak ditemukan
        }

        pausePanel = FindObjectOfType<PausePanel>();
        UpdateInventoryUI();
        toggleInventory = false;
    }


    private void OnEnable()
    {
        openButton.Enable();
        openButton.performed += OnOpenButtonPressed;
    }

    private void OnDisable()
    {
        openButton.performed -= OnOpenButtonPressed;
        openButton.Disable();
    }

    private void OnOpenButtonPressed(InputAction.CallbackContext context)
    {
        toggleInventory = !toggleInventory; // Toggle status buka/tutup inventory
        UpdateInventoryUI();
    }

    void Update()
    {
        ToggleInventory(); // Update tampilan inventory berdasarkan status toggle
    }

    void ToggleInventory()
    {
        // Tampilkan atau sembunyikan inventory sesuai status toggle dan pause panel
        if (toggleInventory && pausePanel.GetPauseStatus())
        {
            inventoryContainer.SetActive(true);
        }
        else
        {
            inventoryContainer.SetActive(false);
        }
    }

    // Method untuk memperbarui tampilan UI inventory
    public void UpdateInventoryUI()
{
    // Bersihkan slot yang lama
    foreach (SlotItemUI slot in slots)
    {
        Destroy(slot.gameObject);
    }
    slots.Clear();

    // Tambahkan slot baru untuk setiap item di inventory
    for (int i = 0; i < maxSlots; i++)
    {
        GameObject newSlot;
        if (i < playerInventory.items.Count)
        {
            // Gunakan prefab slot item dengan item
            newSlot = Instantiate(slotPrefab, inventoryPanel.transform);
        }
        else
        {
            // Gunakan prefab slot kosong (placeholder)
            newSlot = Instantiate(emptySlotPrefab, inventoryPanel.transform);
            newSlot.SetActive(true); // Pastikan slot kosong dalam keadaan aktif
        }

        SlotItemUI slotUI = newSlot.GetComponent<SlotItemUI>();
        if (slotUI != null)
        {
            if (i < playerInventory.items.Count)
            {
                // Set item di slot berdasarkan index
                slotUI.SetItem(playerInventory.items[i], playerInventory, this, i);
            }
            else
            {
                // Slot kosong sebagai placeholder
                slotUI.SetItem(null, playerInventory, this, i);
            }

            slots.Add(slotUI);
        }
        else
        {
            Debug.LogError("SlotItemUI component not found on instantiated slot.");
        }
    }
}


    // Set slot yang dipilih
    public void SetSelectedSlot(SlotItemUI slot)
    {
        if (selectedSlot != null)
        {
            selectedSlot.Deselect(); // Deselect slot sebelumnya
        }
        selectedSlot = slot;
        selectedSlot.Select();

        // Tampilkan informasi item saat slot dipilih
        UpdateItemInfoDisplay(selectedSlot.GetItem());
    }

    private void UpdateItemInfoDisplay(Item selectedItem)
    {
        if (selectedItem != null)
        {
            // Gunakan UIManager untuk menampilkan informasi item yang dipilih
            UIManager.Instance.DisplayItemInfo(selectedItem);
        }
    }

    // Menggunakan item yang dipilih
    public void UseSelectedItem()
    {
        if (selectedSlot != null)
        {
            playerInventory.UseSelectedItem();
        }
    }

    // Drop item yang dipilih
    public void DropSelectedItem()
    {
        if (selectedSlot != null)
        {
            playerInventory.DropSelectedItem();
            UpdateInventoryUI(); // Refresh UI setelah drop item
        }
    }

    // Pindahkan item dari slot asal ke slot tujuan
    public void MoveItem(int fromIndex, int toIndex)
    {
        playerInventory.MoveItem(fromIndex, toIndex);
        UpdateInventoryUI(); // Refresh UI setelah pemindahan item
    }
}
