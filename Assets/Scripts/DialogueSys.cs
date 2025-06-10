using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject dialoguePanel;
    public Image panelImage;
    public string[] lines;
    public Sprite[] speakerSprites;
    public float textSpeed;
    private int index;
    private bool isTyping = false;

    private Color[] speakerColors = { Color.gray, Color.red };

    void Start()
    {
        Debug.Log("Start method called");

        PlayerPrefs.DeleteKey("DialogueShown");

        if (PlayerPrefs.GetInt("DialogueShown", 0) == 0)
        {
            Debug.Log("Starting dialogue");
            dialoguePanel.SetActive(true);
            textComponent.text = string.Empty;
            panelImage.sprite = speakerSprites[index % speakerSprites.Length];

            StartDialogue();
            PlayerPrefs.SetInt("DialogueShown", 1);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Dialogue already shown, skipping...");
            dialoguePanel.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isTyping)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                isTyping = false;
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        Time.timeScale = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = "";
        foreach (char character in lines[index].ToCharArray())
        {
            textComponent.text += character;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
        isTyping = false;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            panelImage.sprite = speakerSprites[index % speakerSprites.Length];
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }


    void EndDialogue()
    {
        Time.timeScale = 1;
        dialoguePanel.SetActive(false);
    }
}