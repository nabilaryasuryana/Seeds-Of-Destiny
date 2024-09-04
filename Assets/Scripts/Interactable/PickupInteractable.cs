using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupInteractable : BaseInteractable
{
    [Header("Pickup Config")]
    public InputAction pickButton;
    public Item item;
    public InventoryUIManager inventoryUIManager;

    protected override void Awake()
    {
        isPlayerNearby = false;
        if (canvas != null)
        {
            canvas.SetActive(false); // Sembunyikan canvas di awal
        }
        // Coba cari InventoryUIManager jika belum di-assign
        if (inventoryUIManager == null)
        {
            inventoryUIManager = FindObjectOfType<InventoryUIManager>();

            // Jika tidak ditemukan, beri peringatan di konsol
            if (inventoryUIManager == null)
            {
                Debug.LogError("InventoryUIManager not found in the scene! Please make sure it exists.");
            }
        }
    }

    private void OnEnable()
    {
        pickButton.Enable();
        pickButton.performed += OnPickButtonPressed;
    }

    private void OnDisable()
    {
        pickButton.performed -= OnPickButtonPressed;
        pickButton.Disable();
    }

    private void OnPickButtonPressed(InputAction.CallbackContext context)
    {
        if (isPlayerNearby)
        {
            HandleInteraction();
        }
    }

    protected override void HandleInteraction()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                InventorySystem inventory = collider.GetComponent<InventorySystem>();
                if (inventory != null)
                {
                    if (inventory.AddItem(item))
                    {
                        if (inventoryUIManager != null)
                        {
                            inventoryUIManager.UpdateInventoryUI();
                        }
                        else
                        {
                            Debug.LogError("InventoryUIManager is not assigned automatically!");
                        }
                        Destroy(gameObject);
                    }
                }
                break;
            }
        }
    }
}
