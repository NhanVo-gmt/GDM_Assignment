using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float delay = 0.5f;
    private Animator anim;
    private BoxCollider2D box;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        
        Invoke("PlayTrap", delay);
    }

    void PlayTrap()
    {
        anim.Play("Trap");
    }

    void Attack()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position, box.size, 0);
        foreach (Collider2D col in cols)
        {
            if (col.TryGetComponent<PlayerHealth>(out PlayerHealth health))
            {
                health.TakeDamage(1);
            }
        }
    }
}
