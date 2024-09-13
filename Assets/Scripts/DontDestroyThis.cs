using UnityEngine;

public class DontDestroyThis : MonoBehaviour
{
    void Awake()
    {
        // Membuat GameObject ini tidak dihancurkan saat scene berubah
        DontDestroyOnLoad(gameObject);
    }
}
