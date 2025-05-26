using System.Collections;
using UnityEngine;

public class HurtSpikes : MonoBehaviour
{
    public int damage = 100;
    public int spawn = 0;

    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        if (spriteRenderer == null || col == null)
        {
            Debug.LogError("Brak SpriteRenderer lub Collider2D na obiekcie: " + gameObject.name);
            return;
        }

        if (spawn != 0)
        {
            StartCoroutine(ChangeSpawnState());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null && spawn != 2)
            {
                playerHealth.TakeDamage(playerHealth.maxHealth);
            }
        }
    }

    private IEnumerator ChangeSpawnState()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            if (spawn == 2)
            {
                spawn = 1;
                spriteRenderer.enabled = true;
                col.enabled = true;
            }
            else if (spawn == 1)
            {
                spawn = 2;
                spriteRenderer.enabled = false;
                col.enabled = false;
            }
        }
    }
}