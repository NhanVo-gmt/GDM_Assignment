using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float chaseSpeed;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        anim.Play("Move");
    }

    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
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

    public void Phase2()
    {
        chaseSpeed = 4f;
        anim.Play("MoveTrans");
    }
}
