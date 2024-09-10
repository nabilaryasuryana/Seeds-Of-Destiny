using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public GameObject loaderUI;
    public Slider progressSlider;

    // Menggunakan nama scene daripada index
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadScene_Coroutine(sceneName));
    }

    public IEnumerator LoadScene_Coroutine(string sceneName)
    {
        progressSlider.value = 0;
        loaderUI.SetActive(true);

        // Mulai loading scene berdasarkan nama
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        float progress = 0;

        while (!asyncOperation.isDone)
        {
            // Perbarui progress slider
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
            progressSlider.value = progress;

            // Jika progress mencapai 90% (0.9f), biarkan scene berganti
            if (progress >= 0.9f)
            {
                progressSlider.value = 1;
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
