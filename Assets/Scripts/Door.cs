using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Item needItem;
    private Collider2D col;

    private bool isUnlocked = false;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        Inventory.Instance.OnAddedItem += UnlockDoor;
    }

    private void UnlockDoor(Item item)
    {
        if (isUnlocked) return;
        if (item == needItem)
        {
            isUnlocked = true;
            //todo unlock
        }
    }
}
