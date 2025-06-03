using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public CanvasGroup fadeGroup;
    public float fadeDuration = 1.5f;

    public void FadeOutAndLoad(string sceneName)
    {
        StartCoroutine(FadeOutRoutine(sceneName));
    }

    private IEnumerator FadeOutRoutine(string sceneName)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }

        // Za�aduj scen� po zako�czeniu fade out
        SceneManager.LoadScene(sceneName);
    }
    public void StartFadeAndLoadLevel()
    {
        FadeOutAndLoad("Demo"); // zmie� "Level1" na nazw� Twojej sceny poziomu
    }
    public void StartFadeOut()
    {
        FadeOut();
    }

    public IEnumerator FadeOut()
    {

        Debug.Log("skibid");
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }

    }
    
}
