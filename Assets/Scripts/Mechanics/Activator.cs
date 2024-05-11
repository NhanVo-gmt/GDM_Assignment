using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public string id;
    public Action OnActivate;

    public bool canDestroy;
    public bool canTurnoffSprite;

    private SpriteRenderer sprite;

    private void OnValidate()
    {
        if (id == String.Empty) id = System.Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        if (SaveLoadManager.Instance.HasKey(id))
        {
            Activate();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SaveLoadManager.Instance.AddKey(id);
            Activate();
        }
    }

    void Activate()
    {
        OnActivate?.Invoke();
        if (canDestroy) Destroy(gameObject, 0.1f);
        if (canTurnoffSprite) sprite.enabled = false;
    }
}
