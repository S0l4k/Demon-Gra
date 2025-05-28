using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    public float attackRange = 2f;
    public float moveSpeed = 2.5f;

    private Transform player;
    private Rigidbody2D rb;
    private Boss boss;
    private BossWeapon bossWeapon;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        bossWeapon = animator.GetComponentInChildren<BossWeapon>();

        if (bossWeapon == null)
            Debug.LogError("BossWeapon not found!");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null) return;

        boss.LookAtPlayer();

        // Ruch bossa w stronê gracza
        Vector2 targetPosition = new Vector2(player.position.x, rb.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        // Sprawdzenie odleg³oœci do ataku
        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            int attackType = Random.Range(0, 3); // 0 - normalny, 1 - teleport, 2 - kamienie

            switch (attackType)
            {
                case 0:
                    bossWeapon.Attack();
                    break;
                case 1:
                    bossWeapon.SpecialAttack1();
                    break;
                case 2:
                    bossWeapon.SpecialAttack2();
                    break;
            }
        }
    }
}