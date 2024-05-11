using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator anim;
    private CircleCollider2D circle;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        circle = GetComponent<CircleCollider2D>();
    }

    void PlayTrap()
    {
        anim.Play("Idle");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayTrap();
        }
    }

    void Attack()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, circle.radius);
        foreach (Collider2D col in cols)
        {
            if (col.TryGetComponent<PlayerHealth>(out PlayerHealth health))
            {
                health.TakeDamage(1);
            }
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
