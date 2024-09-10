using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    public GameObject itemInfoPanel; // Panel untuk menampilkan informasi item
    public TextMeshProUGUI itemInfoText; // Teks untuk informasi item
    public GameObject informationItemPanel; // Panel untuk menampilkan informasi item
    public TextMeshProUGUI informationItemText; // Teks untuk informasi item
    public InputAction toggleInformationItemAction; // Aksi untuk toggle info item

    private bool isItemInfoOpen = false;
    private bool isInformationItemOpen = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        CloseItemInfo();
        CloseInformationItem();
    }

    private void OnEnable()
    {
        toggleInformationItemAction.Enable();
        toggleInformationItemAction.performed += OnToggleInformationItem;
    }

    private void OnDisable()
    {
        toggleInformationItemAction.performed -= OnToggleInformationItem;
        toggleInformationItemAction.Disable();
    }

    private void OnToggleInformationItem(InputAction.CallbackContext context)
    {
        CloseInformationItem();
    }

    private void OpenItemInfo()
    {
        itemInfoPanel.SetActive(true);
        isItemInfoOpen = true;
    }

    private void CloseItemInfo()
    {
        itemInfoPanel.SetActive(false);
        isItemInfoOpen = false;
    }
    private void OpenInformationItem()
    {
        informationItemPanel.SetActive(true);
        isInformationItemOpen = true;
    }

    private void CloseInformationItem()
    {
        informationItemPanel.SetActive(false);
        isInformationItemOpen = false;
    }

    // Metode untuk memperbarui tampilan informasi item berdasarkan item yang dipilih
    public IEnumerator ShowItemInfo(Item selectedItem)
    {
        OpenItemInfo();
        if (selectedItem != null)
        {
            itemInfoText.text = $"Item: {selectedItem.itemName}\nDescription: {selectedItem.description}";
        }
        else
        {
            itemInfoText.text = "No item selected.";
        }

        // Tampilkan selama 5 detik
        yield return new WaitForSeconds(10f);
        CloseItemInfo();
    }

    // Metode untuk menampilkan informasi penggunaan item
    public void ShowUseItemInfo(string informationText)
    {
        informationItemText.text = informationText;
        StartCoroutine(DisplayUseItemInfo());
    }

    // Coroutine untuk menampilkan informasi penggunaan item tanpa batas waktu
    private IEnumerator DisplayUseItemInfo()
    {
        OpenInformationItem();
        yield return null; // Bisa disesuaikan jika perlu efek lain
    }

    // Metode untuk menjalankan ShowItemInfo dengan item yang dipilih
    public void DisplayItemInfo(Item selectedItem)
    {
        StartCoroutine(ShowItemInfo(selectedItem));
    }
}
