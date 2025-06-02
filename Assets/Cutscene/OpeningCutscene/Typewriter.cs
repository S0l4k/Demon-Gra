using TMPro;
using System.Collections;
using UnityEngine;

public class Typewriter : MonoBehaviour
{
    public float delay = 0.05f;
    public TextMeshProUGUI textMesh;
    [TextArea] public string fullText;
    private Coroutine typingCoroutine;

    void OnEnable() => StartTyping(fullText);

    public void StartTyping(string text)
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        fullText = text;
        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        textMesh.text = "";
        foreach (char c in fullText)
        {
            textMesh.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
    public void Skip()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        textMesh.text = fullText;
    }
}
