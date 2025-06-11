using System.Collections;
using UnityEngine;
using Cinemachine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 20;
    public int special1Damage = 40;
    public float attackRange = 1f;
    public float attackCooldown = 2f;
    public float delayBeforeTeleport = 1f; 
    public bool canAttack = true;
    public LayerMask attackMask;

    // Teleportacja
    public Transform groundCheck;   
    public Transform normalHeight;  
    public float moveSpeed = 2f;   

    private Vector3 playerPositionAtStartOfAttack;

    // Kamienie spadaj¹ce
    public GameObject fallingRockPrefab;
    public Transform[] rockSpawnPoints;
    public int numberOfRocks = 5;
    public float minDelayBetweenSpawns = 0.1f;
    public float maxDelayBetweenSpawns = 0.3f;
    public GameObject groundImpactFXPrefab;
    public Transform impactSpawnPoint;
    public CinemachineImpulseSource impulseSource;


    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D bossCollider;

    public GameObject[] platformsToBreak; 
    public float disappearTime = 10f;     
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bossCollider = GetComponent<Collider2D>();
    }

    public void Attack()
    {
        if (!canAttack) return;
        canAttack = false;

        animator.SetTrigger("Melee");

        Collider2D colInfo = Physics2D.OverlapCircle(transform.position + transform.right * 0.7f, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }

        Invoke(nameof(ResetAttack), attackCooldown);
    }

    public void SpecialAttack1()
    {
        if (!canAttack) return;
        canAttack = false;

        Debug.Log("Rozpoczynam specjalny atak: ukrycie pod ziemi¹");
        animator.SetTrigger("Special1"); 
    }

    public void SpecialAttack2()
    {
        if (!canAttack) return;
        canAttack = false;

        Debug.Log("Specjalny atak 2: Deszcz kamieni!");

        animator.SetTrigger("Special2");

        // WYWO£ANIE SHAKE
        TriggerShake();

        StartCoroutine(SpawnFallingRocks());
    }
    public void TriggerShake()
    {
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
    }




    public void OnUndergroundAnimationFinished()
    {
        Debug.Log("Boss koñczy animacjê schowania siê. Ruszam w dó³...");

        if (bossCollider != null)
            bossCollider.enabled = false;

        StartCoroutine(MoveToGroundCheck());
    }

    private IEnumerator MoveToGroundCheck()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z);

        while ((Vector3)transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Boss dotar³ do ziemi. Czekam przed teleportacj¹...");
        yield return new WaitForSeconds(delayBeforeTeleport);

        TeleportUnderPlayer();
    }

    private void TeleportUnderPlayer()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null) return;

        float newX = player.position.x;

       
        Vector3 newPosition = new Vector3(newX, groundCheck.position.y, transform.position.z);
        transform.position = newPosition;

        
        animator.SetTrigger("Appear");
    }

  
    public void OnExitGroundAttackEvent()
    {
        Debug.Log("Boss wyskakuje i zadaje obra¿enia!");

        Collider2D colInfo = Physics2D.OverlapCircle(transform.position, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(special1Damage);
        }
    }

    
    public void OnExitGroundAnimationFinished()
    {
        StartCoroutine(MoveToNormalHeight());
    }

    private IEnumerator MoveToNormalHeight()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, normalHeight.position.y, transform.position.z);

        while ((Vector3)transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Boss wróci³ na powierzchniê.");
        FinalizeAttack();
    }

    private void FinalizeAttack()
    {
        if (bossCollider != null)
            bossCollider.enabled = true;

        canAttack = true;
    }

   

    private void ResetAttack()
    {
        canAttack = true;
    }



    private IEnumerator SpawnFallingRocks()
    {
        int rockCount = Random.Range(10, 16);
        float spawnDuration = 3f;
        int rocksPerWave = 3;
        float waveInterval = 0.5f;
        if (groundImpactFXPrefab != null && impactSpawnPoint != null)
        {
            Instantiate(groundImpactFXPrefab, impactSpawnPoint.position, Quaternion.identity);
        }



        Platform chosenPlatform = null;

        if (platformsToBreak.Length > 0)
        {
            GameObject platformGO = platformsToBreak[Random.Range(0, platformsToBreak.Length)];
            if (platformGO != null)
            {
                chosenPlatform = platformGO.GetComponent<Platform>();
                if (chosenPlatform != null)
                {
                    chosenPlatform.Hide();
                }
            }
        }

       
        for (int wave = 0; wave < rockCount / rocksPerWave + 1; wave++)
        {
            for (int i = 0; i < rocksPerWave; i++)
            {
                if (rockSpawnPoints.Length == 0) yield break;

                Transform randomSpawnPoint = rockSpawnPoints[Random.Range(0, rockSpawnPoints.Length)];
                Instantiate(fallingRockPrefab, randomSpawnPoint.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(Random.Range(0.2f, waveInterval));
        }

       
        if (chosenPlatform != null)
        {
            yield return new WaitForSeconds(disappearTime);
            chosenPlatform.Show();
        }

        ResetAttack();
    }
}