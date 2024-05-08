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
    public AudioClip hitClip;

    public Action<int> OnTakeDamage;
    public Action OnDie;

    private SpriteRenderer sprite;
    private bool canAttack = true;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!canAttack) return;
        
        currentHealth -= damageAmount;
        OnTakeDamage?.Invoke(currentHealth);
        StartBlinking();
        GameManager.Instance.Sleep(0.3f);
        CameraController.Instance.Shake();
        AudioManager.Instance.PlayOneShot(hitClip);
        
        if (currentHealth <= 0)
        {
            OnDie?.Invoke();
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
        canAttack = false;
        yield return new WaitForSeconds(0.1f);
        float startTime = Time.time;

        while (startTime + coolDown > Time.time)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(0.2f);
        }

        yield return null;

        canAttack = true;
        sprite.enabled = true;
    }

}
