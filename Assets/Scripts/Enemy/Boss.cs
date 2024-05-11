using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float chaseSpeed;

    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
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
}
