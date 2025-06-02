using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI textUI;

    [Header("Typewriter Settings")]
    [TextArea] public string fullText;
    public float letterDelay = 0.04f;

    private Coroutine typingCoroutine;

    public void StartTyping()
    {
        // Upewnij siê, ¿e obiekt dialogowy jest aktywny
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText());
    }

    public void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        textUI.text = fullText;
    }

    private IEnumerator TypeText()
    {
        textUI.text = "";
        foreach (char c in fullText)
        {
            textUI.text += c;
            yield return new WaitForSeconds(letterDelay);
        }
    }

    public void ClearText()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        textUI.text = "";

        // (Opcjonalnie) schowaj obiekt po zakoñczeniu
        gameObject.SetActive(false);
    }
}
