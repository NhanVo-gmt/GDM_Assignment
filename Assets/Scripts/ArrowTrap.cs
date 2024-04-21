using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private Projectile prefab;
    [SerializeField] private float speed;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Vector2 attackDirection;
    [SerializeField] private float ShootRate = 3f;
    [SerializeField] private float delay = 0.5f;
    
    private void Awake()
    {
        InvokeRepeating("SpawnArrow", delay, ShootRate);
    }

    public void SpawnArrow()
    {
        Projectile projectile = Instantiate(prefab, spawnPos.position, Quaternion.identity);
        projectile.Initialize(speed, attackDirection.normalized);
    }
}
