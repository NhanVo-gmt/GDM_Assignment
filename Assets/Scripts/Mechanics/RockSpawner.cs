using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> rockList = new List<GameObject>();
    [SerializeField] private float duration;

    private void Awake()
    {
        InvokeRepeating("SpawnRock", Random.Range(0.0f, 3.0f), 2f);
    }

    void SpawnRock()
    {
        GameObject rock = rockList[Random.Range(0, rockList.Count)];
        Instantiate(rock, transform.position, Quaternion.identity);
    }
}
