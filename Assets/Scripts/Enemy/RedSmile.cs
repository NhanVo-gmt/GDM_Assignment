using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSmile : MonoBehaviour, ILightable, IStunable
{
    public float chaseSpeed = 3f;
    public float detectionRadius = 5f;

    private Transform playerTransform;
    private float dist;
    private Vector3 currentTarget;
    private bool isChasing = false;

    private bool isDead = false;
    private bool isTakingLight = false;
    private float timeTakingLightElapse = 0f;
    [SerializeField] private float timeTakingLightElapseMax = 2f;

    private Animator anim;
    private ParticleSystem particleSystem;
    private Collider2D col;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        col = GetComponent<Collider2D>();

        col.enabled = false;
    }
    
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (playerTransform == null) 
        {
            Debug.LogError("Player not found");
        }
    }

    private void Update()
    {
        if (isDead) return;

        if (!isChasing && Vector2.Distance(playerTransform.position, transform.position) <= detectionRadius)
        {
            Spawn();
        }
        
        if (playerTransform != null && isChasing)
        {
            ChasePlayer();
        }

        if (isTakingLight)
        {
            timeTakingLightElapse += Time.deltaTime;
            if (timeTakingLightElapse >= timeTakingLightElapseMax)
            {
                Die();
            }
        }
    }

    private void Spawn()
    {
        col.enabled = true;
        anim.Play("Spawn");
        //todo sound
    }

    void StartChase()
    {
        isChasing = true;
        anim.Play("Move");
    }

    private void ChasePlayer()
    {
        currentTarget = playerTransform.position;
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, chaseSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
            else
            {
                Debug.LogError("PlayerHealth component not found");
            }
        }
    }

    public void Stun()
    {
        Die();
    }

    void Die()
    {
        isDead = true;
        anim.Play("Dead");
        Destroy(gameObject, 1f);
    }
    
    public void TakeLight()
    {
        isTakingLight = true;
        particleSystem.Play();
    }

    public void StopTakeLight()
    {
        isTakingLight = false;
        particleSystem.Stop();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
