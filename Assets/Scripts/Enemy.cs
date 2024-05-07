using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IStunable
{
    public float radius;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3f;
    public float detectionRadius = 5f;

    private Transform playerTransform;
    private float dist;
    private Vector3 currentTarget;
    private bool isChasing = false;

    private bool isStun = false;

    private Vector3 startPos;

    private Animator anim;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        startPos = transform.position;
    }

    void Start()
    {
        currentTarget = (Vector3)Random.insideUnitCircle * radius + startPos;

        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (playerTransform == null ) 
        {
            Debug.LogError("Player not found");
        }
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer < detectionRadius)
            {
                isChasing = true;
            }
            else
            {
                isChasing = false;
            }

            if (isStun) return;

            if (isChasing)
            {
                ChasePlayer();
            }
            else
            {
                Patrol();
            }
        }
    }

    void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentTarget) < 0.1f)
        {
            currentTarget = (Vector3)Random.insideUnitCircle * radius + startPos;
        }
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



}
