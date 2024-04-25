using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 0;
    private Vector2 direction;

    private void Awake()
    {
        Destroy(gameObject, 5f);
    }

    public void Initialize(float speed, Vector2 direction)
    {
        transform.up = this.direction;
        this.speed = speed;
        this.direction = direction;
    }

    private void Update()
    {
        if (speed == 0) return;
        Move();
    }

    void Move()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if (other.TryGetComponent<PlayerHealth>(out PlayerHealth health))
        {
            health.TakeDamage(1);
        }
    }
}
