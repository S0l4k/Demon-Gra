using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damage = 10;
    public float lifetime = 2f;
    public Collider2D catCollider;

    void Start()
    {
        if (catCollider != null)
        {
            Collider2D myCollider = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(myCollider, catCollider);
        }

        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
     
        if (collision.collider.CompareTag("Enemy"))
        {
            
            BasicEnemyController enemyScript = collision.transform.GetComponentInParent<BasicEnemyController>();
            RangeEnemyController rangeEnemyScript = collision.transform.GetComponentInParent<RangeEnemyController>();
            BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damage, transform.position.x);
            }

            if (rangeEnemyScript != null)
            {
                rangeEnemyScript.TakeDamage(damage, transform.position.x);
            }

            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        Debug.Log("Fireball hit: " + collision.gameObject.name);
    }
 

}