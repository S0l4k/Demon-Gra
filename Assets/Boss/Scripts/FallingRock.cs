using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

        // Opcjonalnie: efekt eksplozji lub dŸwiêk
        Destroy(gameObject);
    }
}