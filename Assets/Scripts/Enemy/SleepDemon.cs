using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SleepDemon : MonoBehaviour, ILightable
{
    private Animator anim;
    private bool isSleepAngry;
    private float timeAngryElapse;
    private float timeAngryMax = 2f;

    [SerializeField] private float chaseSpeed = 5f;
    
    [Header("Info")]
    private Vector3 _startPos;
    private float _timer;
    private Vector3 _randomPos;
    
    [Header("Settings")]
    [Range(0f, 2f)]
    public float _time = 0.2f;
    [Range(0f, 2f)]
    public float _distance = 0.1f;
    [Range(0f, 0.1f)]
    public float _delayBetweenShakes = 0f;

    private Transform playerTransform;
    private bool isChasing = false;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        _startPos.x = transform.position.x;
        _startPos.y = transform.position.y;
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
            return;
        }
        
        if (isSleepAngry)
        {
            timeAngryElapse += Time.deltaTime;
            if (timeAngryElapse >= timeAngryMax)
            {
                isChasing = true;
        
                StopShake();
                anim.Play("Move");
            }
        }
        else if (timeAngryElapse > 0f)
        {
            timeAngryElapse -= Time.deltaTime;
        }
    }
    
    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine());
    }

    void StopShake()
    {
        StopAllCoroutines();
    }
    
    private IEnumerator ShakeCoroutine()
    {
        while (isSleepAngry)
        {
            yield return SingleShakeCoroutine();
            yield return new WaitForSeconds(0.5f);
        }
    }
 
    private IEnumerator SingleShakeCoroutine()
    {
        _timer = 0f;
 
        while (_timer < _time)
        {
            _timer += Time.deltaTime;
 
            _randomPos = _startPos + (Random.insideUnitSphere * _distance);
 
            transform.position = _randomPos;
 
            if (_delayBetweenShakes > 0f)
            {
                yield return new WaitForSeconds(_delayBetweenShakes);
            }
            else
            {
                yield return null;
            }
        }
 
        transform.position = _startPos;
    }


    private void ChasePlayer()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, chaseSpeed * Time.deltaTime);
    }

    public void TakeLight()
    {
        if (isChasing) return;
        
        anim.Play("SleepAngry");
        isSleepAngry = true;
        Shake();
        //todo angry sound
    }

    public void StopTakeLight()
    {
        if (isChasing) return;
        
        StopShake();
        isSleepAngry = false;
        anim.Play("Sleep");
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
            else
            {
                Debug.LogError("PlayerHealth component not found");
            }
        }
    }
}
