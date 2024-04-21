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
    
    private void Awake()
    {
        InvokeRepeating("SpawnArrow", 0f, ShootRate);
    }

    public void SpawnArrow()
    {
        Projectile projectile = Instantiate(prefab, spawnPos.position, Quaternion.identity);
        projectile.Initialize(speed, attackDirection.normalized);
    }
}
