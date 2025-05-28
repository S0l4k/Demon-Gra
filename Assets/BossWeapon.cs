using System.Collections;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 20;
    public float attackRange = 1f;
    public float attackCooldown = 2f;
    public bool canAttack = true;
    public LayerMask attackMask;

    // Teleportacja pod gracza
    public Transform groundCheck; // Punkt poni¿ej sceny (np. Y = -2)
    private Vector3 hiddenPosition;
    private bool isHidden = false;

    // Kamienie spadaj¹ce z góry
    public GameObject fallingRockPrefab;
    public Transform[] rockSpawnPoints;
    public int numberOfRocks = 5;
    public float minDelayBetweenSpawns = 0.1f;
    public float maxDelayBetweenSpawns = 0.3f;

    void Awake()
    {
        if (rockSpawnPoints == null || rockSpawnPoints.Length == 0)
        {
            Debug.LogWarning("Nie przypisano punktów spawnu kamieni!");
        }
    }

    public void Attack()
    {
        if (!canAttack) return;
        canAttack = false;

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

        Debug.Log("Specjalny atak 1: Boss znika pod ziemi¹");

        Vector3 playerPos = GetPlayerPosition();
        hiddenPosition = new Vector3(playerPos.x, groundCheck.position.y, transform.position.z);
        transform.position = hiddenPosition;

        isHidden = true;

        // Planujemy atak pojawienia siê
        Invoke(nameof(GroundBreakAttack), 1.5f);
    }

    public void SpecialAttack2()
    {
        if (!canAttack) return;
        canAttack = false;

        Debug.Log("Specjalny atak 2: Kamienie z nieba!");

        SpawnFallingRocks();

        Invoke(nameof(ResetAttack), attackCooldown * 2);
    }

    public void GroundBreakAttack()
    {
        if (!isHidden)
        {
            Debug.LogWarning("GroundBreakAttack anulowany – boss nie jest ukryty");
            ResetAttack();
            return;
        }

        if (!canAttack)
        {
            Debug.LogWarning("GroundBreakAttack anulowany – cooldown trwa");
            isHidden = false;
            ResetAttack();
            return;
        }

        Debug.Log("GroundBreakAttack: Boss wyskakuje z ziemi!");

        // Wracamy na powierzchniê
        Vector3 surfacePosition = new Vector3(hiddenPosition.x, groundCheck.position.y + 1.5f, transform.position.z);
        transform.position = surfacePosition;

        // Wykonujemy atak wrêcz
        Collider2D colInfo = Physics2D.OverlapCircle(transform.position, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage * 2); // mocniejszy atak przy wyjœciu
        }

        isHidden = false;
        ResetAttack();
    }

    private void SpawnFallingRocks()
    {
        StartCoroutine(SpawnRocksCoroutine());
    }

    private IEnumerator SpawnRocksCoroutine()
    {
        for (int i = 0; i < numberOfRocks; i++)
        {
            if (rockSpawnPoints.Length == 0) yield break;

            Transform randomSpawnPoint = rockSpawnPoints[Random.Range(0, rockSpawnPoints.Length)];
            Instantiate(fallingRockPrefab, randomSpawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(minDelayBetweenSpawns, maxDelayBetweenSpawns));
        }
    }

    private Vector3 GetPlayerPosition()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("Nie znaleziono gracza!");
            return transform.position;
        }

        return player.position;
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.right * 0.7f, attackRange);
    }
}