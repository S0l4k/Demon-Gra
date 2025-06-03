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


    public float nextLevelDelay = 3f; // <- po ilu sekundach przejœæ dalej
    public string nextSceneName; // <- nazwa sceny z kolejnym poziomem

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        currentHealth = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
       

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
        if (spriteRenderer != null) ;
           
    }

    void Die()
    {
        animator?.SetTrigger("Death");
        // np. animacja œmierci, efekty cz¹steczkowe itp.
        Destroy(gameObject, 1f); // usuniêcie obiektu po 1s
        Invoke(nameof(LoadNextLevel), nextLevelDelay);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(2);
    }
}
