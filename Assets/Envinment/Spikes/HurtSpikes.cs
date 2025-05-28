using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtSpikes : MonoBehaviour
{
    public int damage = 15;
    public int spawn = 1;

    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    private Color originalColor;
    private Color fadedColor;

    private HashSet<GameObject> cooldownPlayers = new HashSet<GameObject>();
    public float damageCooldown = 2f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        if (spriteRenderer == null || col == null)
        {
            Debug.LogError("Brak SpriteRenderer lub Collider2D na obiekcie: " + gameObject.name);
            return;
        }

        originalColor = spriteRenderer.color;
        fadedColor = new Color(originalColor.r * 0.5f, originalColor.g * 0.5f, originalColor.b * 0.5f, 0.5f);

        StartCoroutine(StateCycle());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (spawn != 1) return;

        if (collision.gameObject.CompareTag("Player") && !cooldownPlayers.Contains(collision.gameObject))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                cooldownPlayers.Add(collision.gameObject);
                StartCoroutine(RemoveCooldown(collision.gameObject));
            }
        }
    }

    private IEnumerator RemoveCooldown(GameObject obj)
    {
        yield return new WaitForSeconds(damageCooldown);
        cooldownPlayers.Remove(obj);
    }

    private IEnumerator StateCycle()
    {
        while (true)
        {
            spawn = 1;
            col.enabled = true;
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(5f);

            yield return StartCoroutine(BlinkWarning(3f));

            spawn = 2;
            col.enabled = false;
            spriteRenderer.color = fadedColor;
            yield return new WaitForSeconds(5f);

            yield return StartCoroutine(BlinkWarning(3f));
        }
    }

    private IEnumerator BlinkWarning(float duration)
    {
        float blinkInterval = 0.2f;
        float timer = 0f;
        bool visible = true;

        while (timer < duration)
        {
            spriteRenderer.enabled = visible;
            visible = !visible;

            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        spriteRenderer.enabled = true;
    }
}