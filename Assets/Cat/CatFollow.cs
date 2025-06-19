using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class CatFollow : MonoBehaviour
{
    public AudioClip[] meowSounds;
    private AudioSource audioSource;
    public Transform player;
    public float followSpeed = 5f;
    public float distanceThreshold = 0.5f;
    
    public float minMeowTime = 3f;
    public float maxMeowTime = 10f;

    private Rigidbody2D rb;
    private Animator anim;

    private bool isFlying = false;
    private bool isMoving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Wy³¹czenie fizyki, by nie przeszkadza³a w lataniu
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.gravityScale = 0f;
        }

        if (AudioManager.Instance != null)
        {
            AudioMixer mixer = AudioManager.Instance.audioMixer;
            var groups = mixer.FindMatchingGroups("Master");
            if (groups.Length > 0)
                audioSource.outputAudioMixerGroup = groups[0];
        }

        StartCoroutine(MeowRoutine());
    }

    private void Update()
    {
        FollowPlayer();
       
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = player.position;
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance > distanceThreshold)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // Jeœli nie by³ wczeœniej w trybie lotu, uruchom animacjê transformacji
        if (!isFlying)
        {
            anim.SetTrigger("transformToFlying");
            anim.SetBool("isAirborne", true); // <- dodajemy to
            isFlying = true;
        }

        anim.SetBool("isFlying", isMoving); // animacja "w ruchu" lub idle flying
        FlipTowardsPlayer();
    }



    private void FlipTowardsPlayer()
    {
        if (player.position.x > transform.position.x && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (player.position.x < transform.position.x && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private IEnumerator MeowRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minMeowTime, maxMeowTime);
            yield return new WaitForSeconds(waitTime);
            PlayMeow();
        }
    }

    private void PlayMeow()
    {
        if (meowSounds.Length > 0 && audioSource != null)
        {
            AudioClip clip = meowSounds[Random.Range(0, meowSounds.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore kolizje z graczem lub wrogami
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
    }
}
