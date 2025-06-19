using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    public Animator anim;

    public bool bloodFull;
    public int bloodResource = 0;
    public int maxBloodResource = 100;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float specialAttackRange = 5f;

    public LayerMask enemyLayers;

    // Attack 1
    public int attackDamage1 = 30;
    public float attackRate1 = 2f;

    // Attack 2
    public int attackDamage2 = 60;
    public float attackRate2 = 1f;

    // Attack 3 (fireball)
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireballSpeed = 10f;
    public int fireballDamage = 20;
    public float attackRate3 = 5f;

    //Special Attack
    public int specialAttackDamage = 100;
    public GameObject bloodExplosionPrefab;



    private float nextAttackTime1 = 0f;
    private float nextAttackTime2 = 0f;
    private float nextAttackTime3 = 0f;

    public AudioSource audioSource;
    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip attack3;

    private PlayerResource playerResource;


    private void Start()
    {
        bloodFull = false;
        playerResource = GetComponent<PlayerResource>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        bloodFull = bloodResource >= maxBloodResource;
        if (Time.time >= nextAttackTime1 && Input.GetKeyDown(KeyCode.H))
        {
            Attack1();
            nextAttackTime1 = Time.time + 1f / attackRate1;
        }

        if (Time.time >= nextAttackTime2 && Input.GetKeyDown(KeyCode.J))
        {
            Attack2();
            nextAttackTime2 = Time.time + 1f / attackRate2;
        }

        if (Time.time >= nextAttackTime3 && Input.GetKeyDown(KeyCode.K))
        {
            Attack3();
            nextAttackTime3 = Time.time + 1f / attackRate3;
        }

        if (Input.GetKeyDown(KeyCode.X) && playerResource.CanUseSpecialAttack())
        {
            SpecialAttack();
            playerResource.UseSpecialAttack();
        }
    }

    private void Attack1()
    {
        anim.SetTrigger("Attack1");
        DealBasicDamage(attackDamage1);
        PlayAttack1Sound();
    }

    private void Attack2()
    {
        anim.SetTrigger("Attack2");
        DealBasicDamage(attackDamage2);
        PlayAttack2Sound();
    }

    private void Attack3()
    {
        anim.SetTrigger("Attack3");
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * fireballSpeed;
        PlayAttack3Sound();
    }

    private void SpecialAttack()
    {
        Debug.Log("specialattack");
        anim.SetTrigger("SpecialAttack");

        
        if (bloodExplosionPrefab != null)
        {
            Instantiate(bloodExplosionPrefab, transform.position, Quaternion.identity);
        }

        DealSpecialDamage(specialAttackDamage);
    }


    private void DealBasicDamage(int damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            BasicEnemyController basicEnemy = enemy.transform.parent?.GetComponent<BasicEnemyController>();
            RangeEnemyController rangeEnemy = enemy.transform.parent?.GetComponent<RangeEnemyController>();
            BossHealth bossHealth = enemy.GetComponent<BossHealth>(); 

            if (basicEnemy != null)
            {
                basicEnemy.TakeDamage(damage, transform.position.x);
            }
            else if (rangeEnemy != null)
            {
                rangeEnemy.TakeDamage(damage, transform.position.x);
            }
            else if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
            }
        }
    }
    private void DealSpecialDamage(int damage)
    {
        Collider2D[] hitEnemies=Physics2D.OverlapCircleAll(attackPoint.position, specialAttackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            BasicEnemyController basicEnemy = enemy.transform.parent?.GetComponent<BasicEnemyController>();
            RangeEnemyController rangeEnemy = enemy.transform.parent?.GetComponent<RangeEnemyController>();
            BossHealth bossHealth = enemy.GetComponent<BossHealth>();

            if (basicEnemy != null)
            {
                basicEnemy.TakeDamage(damage, transform.position.x);
            }
            else if (rangeEnemy != null)
            {
                rangeEnemy.TakeDamage(damage, transform.position.x);
            }
            else if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, specialAttackRange);
    }
    private void PlayAttack1Sound()
    {
        if (audioSource != null)
        {

            audioSource.PlayOneShot(attack1);
        }
    }
    private void PlayAttack2Sound()
    {
        if (audioSource != null)
        {

            audioSource.PlayOneShot(attack2);
        }
    }
    private void PlayAttack3Sound()
    {
        if (audioSource != null)
        {

            audioSource.PlayOneShot(attack3);
        }
    }


}
