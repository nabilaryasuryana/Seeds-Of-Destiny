using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFadeWithSceneTransition : UIFade
{
    [SerializeField] private string sceneToLoad;

    public void TransitionToScene(string sceneName)
    {
        sceneToLoad = sceneName;
        FadeToBlack();
        StartCoroutine(WaitForFadeThenLoadScene());
    }

    private IEnumerator WaitForFadeThenLoadScene()
    {
        // Tunggu sampai fade selesai
        yield return new WaitUntil(() => Mathf.Approximately(fadeScreen.color.a, 1));

        // Load scene baru
        SceneManager.LoadScene(sceneToLoad);

        Destroy(gameObject);

        // Setelah load scene baru, fade to clear
        FadeToClear();
    }
}
