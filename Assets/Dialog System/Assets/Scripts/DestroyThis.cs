using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Penting untuk mengakses scene manager

public class DestroyThis : MonoBehaviour
{
    // Referensi untuk GameObject yang akan dihancurkan
    public GameObject targetObject;
    public string nameScene; // Nama scene yang digunakan sebagai patokan untuk menghancurkan game object

    // Update is called once per frame
    void Update()
    {
        // Cek jika scene aktif adalah scene yang ditentukan
        if (SceneManager.GetActiveScene().name == nameScene)
        {
            DestroyTarget();
        }
    }

    public void DestroyTarget()
    {
        if (targetObject != null)
        {
            // Menghancurkan game object meskipun telah diberi DontDestroyOnLoad
            Destroy(targetObject);
            Debug.Log(targetObject.name + " has been destroyed in scene: " + nameScene);
        }
        else
        {
            Debug.LogWarning("Target object is null or not assigned.");
        }
    }
}
