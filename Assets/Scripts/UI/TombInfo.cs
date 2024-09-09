using UnityEngine;
using UnityEngine.UI; // Pastikan ini di-import untuk UI

public class TombInfo : MonoBehaviour
{
    public GameObject player;      // Referensi ke objek player
    public Canvas infoCanvas;      // Canvas yang akan dibuka saat player mendekat
    public Canvas otherCanvas;     // Canvas yang akan ditutup saat player mendekat
    public GameObject infoImage;   // GameObject Image yang berada di luar Canvas
    public float detectionRadius = 5f; // Jarak deteksi player dari makam

    private bool isPlayerNearby = false;

    void Start()
    {
        // Pastikan Canvas dan Image dimatikan di awal permainan
        infoCanvas.gameObject.SetActive(false);
        infoImage.SetActive(false); // Jika Image adalah GameObject yang terpisah dari Canvas
    }

    void Update()
    {
        // Hitung jarak antara player dan makam
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Jika player berada dalam radius yang ditentukan
        if (distance < detectionRadius && !isPlayerNearby)
        {
            infoCanvas.gameObject.SetActive(true);  // Aktifkan Canvas untuk penjelasan makam
            infoImage.SetActive(true);              // Aktifkan Image yang terpisah
            otherCanvas.gameObject.SetActive(false); // Matikan Canvas lain
            isPlayerNearby = true;
        }
        // Jika player menjauh dari radius, kembalikan ke semula
        else if (distance >= detectionRadius && isPlayerNearby)
        {
            infoCanvas.gameObject.SetActive(false);  // Matikan Canvas untuk penjelasan makam
            infoImage.SetActive(false);              // Matikan Image yang terpisah
            otherCanvas.gameObject.SetActive(true);  // Aktifkan kembali Canvas lain
            isPlayerNearby = false;
        }
    }
}
