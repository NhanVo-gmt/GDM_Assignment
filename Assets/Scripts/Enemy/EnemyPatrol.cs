using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour, IStunable
{
    public Transform[] waypoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3f;
    public float detectionRadius = 5f;

    private Transform playerTransform;
    private int waypointIndex = 0;
    private float dist;
    private Vector3 currentTarget;
    private bool isChasing = false;

    private bool isStun = false;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        currentTarget = waypoints[waypointIndex].position;

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
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
            currentTarget = waypoints[waypointIndex].position;
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
        if (anim) anim.Play("Idle");
        yield return new WaitForSeconds(2f);

        if (anim) anim.Play("Move");
        isStun = false;
    }


}
