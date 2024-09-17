using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnAnimationEnd : MonoBehaviour
{
    public string sceneToLoad; // Nama scene yang akan dimuat
    private Animator animator; // Animator component

    void Start()
    {
        // Ambil komponen Animator dari GameObject ini
        animator = GetComponent<Animator>();
        
        if (animator == null)
        {
            Debug.LogError("Animator component not found.");
        }
    }

    // Metode ini akan dipanggil dari event animasi
    public void OnAnimationEnd()
    {
        LoadScene();
    }

    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Muat scene berdasarkan nama
            SceneManager.LoadScene(sceneToLoad);
            Debug.Log("Loading scene: " + sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is not specified.");
        }
    }
}
