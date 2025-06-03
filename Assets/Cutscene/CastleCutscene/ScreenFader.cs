using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class ScreenFader : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
       
    }

    public void FadeOutNoArgs() // ← to wywołujesz z Timeline
    {
        StartFadeOut(1f);
    }

    public void StartFadeOut(float duration = 1f, string nextSceneName = "")
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeOutCoroutine(duration, nextSceneName));
    }

    private IEnumerator FadeOutCoroutine(float duration, string nextSceneName)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, time / duration);
            yield return null;
        }

        canvasGroup.alpha = 1f;

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
    public void FadeInNoArgs() // ← do użycia w Timeline
    {
        StartFadeIn(1f);
    }

    public void StartFadeIn(float duration = 1f)
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeInCoroutine(duration));
    }

    private IEnumerator FadeInCoroutine(float duration)
    {
        float time = 0f;
        canvasGroup.alpha = 1f;

        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / duration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

}
