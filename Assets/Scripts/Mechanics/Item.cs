using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public enum ItemName
    {
        SilverKey,
        GoldKey,
        Potion
    }

    public ItemName itemName;
    
}
