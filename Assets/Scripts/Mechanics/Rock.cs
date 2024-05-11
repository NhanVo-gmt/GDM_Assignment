using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    private BoxCollider2D box;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rock"))
        {
            AttackPlayer();
            Destroy(parent);
        }
    }

    private void AttackPlayer()
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
