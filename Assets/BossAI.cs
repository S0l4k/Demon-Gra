using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float meleeRange = 1.5f;
    public float special1Range = 5f;
    public float special2Range = 8f;
    public float moveSpeed = 2.5f;

    public float specialAttackCooldown = 5f; 

    private Transform player;
    private Rigidbody2D rb;
    private Boss boss;
    private BossWeapon bossWeapon;

    private float lastSpecialAttackTime;
    private int lastAttackType = -1; 
    private bool isAttacking = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<Boss>();
        bossWeapon = GetComponentInChildren<BossWeapon>();

        if (player == null)
            Debug.LogError("Nie znaleziono gracza z tagiem 'Player'!");

        if (rb == null)
            Debug.LogError("Brak Rigidbody2D na bossie!");

        if (bossWeapon == null)
            Debug.LogError("BossWeapon not found!");
    }

    void FixedUpdate()
    {
        if (player == null || isAttacking) return;

        boss.LookAtPlayer();

        Vector2 targetPosition = new Vector2(player.position.x, rb.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

        Debug.DrawLine(rb.position, targetPosition, Color.red);

        rb.MovePosition(newPosition);
    }

    void Update()
    {
        if (player == null || bossWeapon == null || !bossWeapon.canAttack || Time.time < lastSpecialAttackTime + specialAttackCooldown)
            return;

        float distanceToPlayer = Vector2.Distance(player.position, rb.position);

        
        if (distanceToPlayer <= meleeRange && !isAttacking)
        {
            StartCoroutine(PerformMeleeAttack());
            return; 
        }

        
        if (distanceToPlayer <= special2Range && !isAttacking)
        {
            int attackType = ChooseRandomSpecialAttack();
            switch (attackType)
            {
                case 0:
                    StartCoroutine(PerformSpecialAttack1());
                    break;
                case 1:
                    StartCoroutine(PerformSpecialAttack2());
                    break;
            }
        }
    }

    int ChooseRandomSpecialAttack()
    {
        int attackType;
        
        do
        {
            attackType = Random.Range(0, 2);
        } while (attackType == lastAttackType && Random.value > 0.3f); 

        lastAttackType = attackType;
        return attackType;
    }

    IEnumerator PerformMeleeAttack()
    {
        isAttacking = true;
        bossWeapon.Attack();
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        lastSpecialAttackTime = Time.time;
    }

    IEnumerator PerformSpecialAttack1()
    {
        isAttacking = true;
        bossWeapon.SpecialAttack1();

        yield return new WaitForSeconds(1.5f); 
        isAttacking = false;
        lastSpecialAttackTime = Time.time;
    }

    IEnumerator PerformSpecialAttack2()
    {
        isAttacking = true;
        bossWeapon.SpecialAttack2();
        yield return new WaitForSeconds(1.5f); 
        isAttacking = false;
        lastSpecialAttackTime = Time.time;
    }
    private void OnDrawGizmosSelected()
    {
        // Atak wręcz
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);

        // Specjalny atak 1 (teleportacja / teleportacja)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, special1Range);

        // Specjalny atak 2 (kamienie)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, special2Range);
    }
}