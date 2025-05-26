using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 20;
    public int enragedAttackDamage = 35;
    public int aoeDamage = 25;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public float aoeRange = 3f;
    public float attackCooldown = 2f;
    private bool canAttack = true;
    public LayerMask attackMask;

    private Animator animator;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void Attack()
    {
        if (!canAttack) return;
        canAttack = false;
        Invoke(nameof(ResetAttack), attackCooldown);

        Vector3 pos = GetAttackPosition();
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    public void EnragedAttack()
    {
        Debug.Log("wkurzony");
        if (!canAttack) return;
        canAttack = false;
        Invoke(nameof(ResetAttack), attackCooldown);

        animator?.SetTrigger("attack1");

        Vector3 pos = GetAttackPosition();
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange + 0.5f, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(enragedAttackDamage);
        }
    }

    public void EnragedAOEAttack()
    {
        Debug.Log("obszarowka");
        if (!canAttack) return;
        canAttack = false;
        Invoke(nameof(ResetAttack), attackCooldown);

        animator?.SetTrigger("AOE");

        Vector3 pos = transform.position;
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(pos, aoeRange, attackMask);
        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(aoeDamage);
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    private Vector3 GetAttackPosition()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        return pos;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = GetAttackPosition();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aoeRange);
    }
}
