using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // do Image

public class KingInteraction : MonoBehaviour
{
    public float hugDuration = 2.5f;
    public GameObject redFlashPanel;
    public Animator kingAnimator;

    public TypewriterEffect bossDialog; // Pod³¹cz tutaj swój komponent dialogowy
    public string deathDialogText = "I'm sorry, my subjects. I have failed you all.";

    public Image fadePanel;  // Czarny panel do fade out (Image z Canvas)

    public float fadeDuration = 2f;

    private bool hasInteracted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasInteracted) return;

        if (collision.CompareTag("Fireball"))
        {
            hasInteracted = true;
            StartCoroutine(HandleInteraction(collision.gameObject));
        }
    }

    private IEnumerator HandleInteraction(GameObject player)
    {
        // Wy³¹cz ruch gracza
        var movement = player.GetComponent<PlayerController>();
        if (movement != null) movement.enabled = false;

        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero;

        // Odtwórz animacjê przytulenia króla
        if (kingAnimator != null) kingAnimator.SetTrigger("Hug");

        // Odtwórz animacjê gracza
        Animator playerAnimator = player.GetComponent<Animator>();
        if (playerAnimator != null) playerAnimator.SetTrigger("Hug");

        // Czekaj w przytuleniu
        yield return new WaitForSeconds(hugDuration);

        // Miganie ekranu na czerwono
        if (redFlashPanel != null)
        {
            redFlashPanel.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            redFlashPanel.SetActive(false);
        }

        // Animacja œmierci króla
        if (kingAnimator != null) kingAnimator.SetTrigger("Death");

        // Uruchom dialog bossa
        if (bossDialog != null)
        {
            bossDialog.fullText = deathDialogText;
            bossDialog.StartTyping();

            float waitTime = deathDialogText.Length * bossDialog.letterDelay + 2f;
            yield return new WaitForSeconds(waitTime);

            bossDialog.ClearText();
        }

        // Fade out ekranu
        if (fadePanel != null)
        {
            yield return StartCoroutine(FadeOut());
        }

        // Opcjonalnie: po fade oucie mo¿na zrobiæ coœ dalej, np. za³adowaæ scenê
    }

    private IEnumerator FadeOut()
    {
        float elapsed = 0f;
        Color color = fadePanel.color;
        color.a = 0f;
        fadePanel.color = color;
        fadePanel.gameObject.SetActive(true);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeDuration);
            fadePanel.color = color;
            yield return null;
        }

        color.a = 1f;
        fadePanel.color = color;
    }
}
