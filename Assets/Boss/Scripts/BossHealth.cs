using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int health = 400;
    public int currentHealth;
    public bool isEnraged = false;
    public float enragedThreshold = 150f;

    public Slider healthBar;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public float nextLevelDelay = 3f;
    public string nextSceneName;

    [Header("Audio")]
    public AudioClip hurtClip;
    public AudioClip deathClip;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        currentHealth = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (healthBar != null)
        {
            healthBar.maxValue = health;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        animator?.SetTrigger("Hurt");

        currentHealth -= damage;
        if (healthBar != null)
            healthBar.value = currentHealth;

        PlaySound(hurtClip);

        if (!isEnraged && currentHealth <= enragedThreshold)
        {
            isEnraged = true;
            BecomeEnraged();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void BecomeEnraged()
    {
        Debug.Log("Boss is now enraged!");
        // Dodaj inne efekty, np. zmianê koloru, przyspieszenie animacji itd.
    }

    void Die()
    {
        animator?.SetTrigger("Death");

        PlaySound(deathClip);

        Destroy(gameObject, 5f);
        Invoke(nameof(LoadNextLevel), nextLevelDelay);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextSceneName); // u¿ycie nazwy zamiast indeksu
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
