using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private List<Item> itemList = new List<Item>();
    
    public Action<Item> OnAddedItem;
    public void AddItem(Item item)
    {
        itemList.Add(item);
        OnAddedItem?.Invoke(item);
    }

    public void RemoveItem(Item item)
    {
        int index = itemList.IndexOf(item);
        if (index != -1)
        {
            itemList.RemoveAt(index);
        }
    }
}
