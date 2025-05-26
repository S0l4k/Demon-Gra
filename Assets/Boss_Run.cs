using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    public float attackRange = 2f;
    public float normalSpeed = 2.5f;
    public float enragedSpeed = 4f;

    float currentSpeed;
    Transform player;
    Rigidbody2D rb;
    Boss boss;
    BossWeapon bossWeapon;
    BossHealth bossHealth;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        bossWeapon = animator.GetComponentInChildren<BossWeapon>();
        bossHealth = animator.GetComponent<BossHealth>();

        if (bossWeapon == null)
        {
            Debug.LogError("BossWeapon not found! Make sure it's a child of the boss GameObject.");
        }

        currentSpeed = normalSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, currentSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("attack1");

            if (bossHealth.isEnraged)
            {
                // losowy wybór ataku
                int attackType = Random.Range(0, 2); // 0 lub 1

                if (attackType == 0)
                    bossWeapon.EnragedAttack();
                else
                    bossWeapon.EnragedAOEAttack();
            }
            else
            {
                bossWeapon.Attack();
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack1");
    }

    public void BecomeEnraged()
    {
        currentSpeed = enragedSpeed;
    }
}
