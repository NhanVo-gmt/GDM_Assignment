using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider2D col;

    private bool isUnlocked = false;

    [SerializeField] private List<Activator> Activators = new List<Activator>();
    private int numUnlock = 0;

    private Animator anim;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        foreach (Activator act in Activators)
        {
            act.OnActivate += UnlockDoor;
        }
    }

    
    private void UnlockDoor()
    {
        numUnlock++;
        if (numUnlock >= Activators.Count)
        {
            Destroy(this.gameObject);
        }
    }
}
