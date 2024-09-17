using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // Nama scene yang akan dimuat
    [SerializeField] private string sceneTransitionName; // Nama transisi scene

    public bool offOnAwake = false; // Jika true, nonaktifkan komponen saat Awake

    private float waitToLoadTime = 1f; // Waktu tunggu sebelum scene dimuat

    void Awake()
    {
        // Nonaktifkan komponen jika offOnAwake adalah true
        if (offOnAwake)
        {
            this.enabled = false; // Menonaktifkan skrip
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Periksa jika objek yang masuk adalah pemain
        if (other.GetComponent<PlayerController>() != null)
        {
            // Set nama transisi scene
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            UIFade.Instance.FadeToBlack(); // Fade to black

            // Mulai coroutine untuk memuat scene
            StartCoroutine(LoadSceneRoutine());
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        // Tunggu waktu yang ditentukan
        yield return new WaitForSeconds(waitToLoadTime);

        // Muat scene yang ditentukan
        SceneManager.LoadScene(sceneToLoad);
    }
}
