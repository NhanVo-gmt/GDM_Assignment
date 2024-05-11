using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreasureEnemy : MonoBehaviour, IStunable, ILightable
{
    public float chaseSpeed = 3f;
    public float detectionRadius = 2f;
    public float chasingRadius = 2f;

    private Transform playerTransform;

    private bool isStun = false;
    private bool isChasing = false;
    private bool isTakingLight = false;

    private float takeLightTime = 0f;
    private float takeLightMax = 2f;

    private Animator anim;
    private ParticleSystem particle;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (playerTransform == null ) 
        {
            Debug.LogError("Player not found");
        }
    }

    private void Update()
    {
        if (isTakingLight)
        {
            takeLightTime += Time.deltaTime;
            if (takeLightTime >= takeLightMax)
            {
                anim.Play("Move");
                isChasing = true;
                takeLightTime = 0f;
                isTakingLight = false;
                particle.Stop();
            }
        }
        
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer < detectionRadius)
            {
                anim.Play("Move");
                isChasing = true;
            }
            
            if (isChasing && distanceToPlayer > 3 * detectionRadius)
            {
                isChasing = false;
                anim.Play("Idle");
            }

            if (isStun) return;

            if (isChasing)
            {
                ChasePlayer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
        }
    }
    
    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, chaseSpeed * Time.deltaTime);
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
        StartCoroutine(StunCoroutine());
    }

    IEnumerator StunCoroutine()
    {
        isStun = true;
        anim.Play("Idle");
        yield return new WaitForSeconds(2f);

        anim.Play("Move");
        isStun = false;
    }


    public void TakeLight()
    {
        isTakingLight = true;
        particle.Play();
    }

    public void StopTakeLight()
    {
        isTakingLight = false;
        particle.Stop();
    }
}

