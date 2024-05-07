using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public Action OnActivate;

    public bool canDestroy;
    public bool canTurnoffSprite;

    private SpriteRenderer sprite;
    
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnActivate?.Invoke();
        if (canDestroy) Destroy(gameObject, 0.1f);
        if (canTurnoffSprite) sprite.enabled = false;
    }

}
