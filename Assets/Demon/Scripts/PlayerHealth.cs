using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator anim;
    public Vector2 respawnPoint;
    public float respawnTime = 3f;
    public Rigidbody2D rb;
    private SpriteRenderer sprite;
    private PlayerController playerController;
    private bool canMove = true;
    public AudioClip hurt;
    public AudioClip die;
    public AudioSource audioSource;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        respawnPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        sprite = rb.GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage() wywo³ane. canMove: " + canMove + ", currentHealth: " + currentHealth);

        if (!canMove || currentHealth <= 0)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            PlayHurtSound();
            anim.SetTrigger("Hurt");
        }
    }

    private void Die()
    {
        PlayDeathSound();
        canMove = false;

        if (playerController != null)
            playerController.SetCanMove(false);

        rb.velocity = Vector2.zero;
        rb.simulated = false;
        anim.SetTrigger("Die");

        StartCoroutine(PlayDeathAnimation());
    }

    private IEnumerator PlayDeathAnimation()
    {
        yield return new WaitForSeconds(0.8f);
        sprite.enabled = false;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(respawnTime);

        transform.position = respawnPoint;

        sprite.enabled = true;
        currentHealth = maxHealth;
        transform.localScale = Vector3.one;
        rb.simulated = true;

        yield return new WaitForSeconds(0.5f);

        canMove = true;
        if (playerController != null)
            playerController.SetCanMove(true);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Respawn"))
        {
            respawnPoint = collider.transform.position;
        }
    }

    public bool CanMove()
    {
        return canMove;
    }

    public void PlayHurtSound()
    {
        if (audioSource != null && hurt != null)
            audioSource.PlayOneShot(hurt);
    }

    public void PlayDeathSound()
    {
        if (audioSource != null && die != null)
            audioSource.PlayOneShot(die);
    }
}