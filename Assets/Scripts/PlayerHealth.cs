using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public float coolDown = 3f;

    public Action<int> OnTakeDamage;

    private SpriteRenderer sprite;
    private Collider2D col;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        OnTakeDamage?.Invoke(currentHealth);
        StartBlinking();
        GameManager.Instance.Sleep(0.3f);
        CameraController.Instance.Shake();
        
        if (currentHealth <= 0)
        {
            Debug.Log("Player has died!");
        }
    }
    

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
    
    public void StartBlinking()
    {
        StartCoroutine(Blinking());
    }

    IEnumerator Blinking()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.1f);
        float startTime = Time.time;

        while (startTime + coolDown > Time.time)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(0.2f);
        }

        yield return null;

        col.enabled = true;
        sprite.enabled = true;
    }

}
