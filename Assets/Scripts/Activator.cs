using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public Action OnActivate;
    private bool isInRange;
    private bool isActivate = false;

    public bool canDestroy;
    public bool canTurnoffSprite;

    private SpriteRenderer sprite;
    
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isActivate) return;
        if (!isInRange) return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            isActivate = true;
            OnActivate?.Invoke();
            if (canDestroy) Destroy(gameObject, 0.1f);
            if (canTurnoffSprite) sprite.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
