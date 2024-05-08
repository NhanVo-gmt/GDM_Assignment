using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player2 : MonoBehaviour
{
    public AnimationCurve curve;

    public float cooldown;
    private float coolDownElapsed = -99f;

    public float radius = 5f;
    
    [Header("Light component")]
    private float startInner = 0f;
    private float startOuter = 5f;
    private float startIntensity = 2f;

    private float endInner = 5f;
    private float endOuter = 10f;
    private float endIntensity = 5f;
    
    public float activateDuration;

    private Light2D light2D;
    private Collider2D col;
    private bool turnOff = false;

    [SerializeField] private EnergyUI energyUI;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        light2D = GetComponentInChildren<Light2D>();
        energyUI = FindObjectOfType<EnergyUI>();
    }

    void Update()
    {
        Move();
        Activate();
        ToggleLight();
    }

    private void ToggleLight()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            turnOff = !turnOff;
            if (turnOff)
            {
                light2D.intensity = 0f;
                col.enabled = false;
                // todo play music
            }
            else
            {
                light2D.intensity = startIntensity;
                col.enabled = true;
            }
        }
    }

    void Move()
    {
        transform.position = GetMouseWorldPosition(Input.mousePosition);
    }

    Vector2 GetMouseWorldPosition(Vector2 mousePos)
    {
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    void Activate()
    {
        if (turnOff) return;
        
        if (coolDownElapsed + cooldown > Time.time)
        {
            UpdateEnergyUI();
            return;
        }
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            coolDownElapsed = Time.time;
            StartCoroutine(ActivateLightCoroutine());
        }
    }

    void UpdateEnergyUI()
    {
        energyUI.UpdateEnergyUI(Mathf.Min(1, (Time.time - coolDownElapsed) / cooldown));
    }

    IEnumerator ActivateLightCoroutine()
    {
        float startTime = Time.time;
        while (startTime + activateDuration >= Time.time)
        {
            float curveValue = curve.Evaluate(Time.time - startTime / activateDuration);
            light2D.intensity = Mathf.Lerp(startIntensity, endIntensity, curveValue);
            light2D.pointLightInnerRadius = Mathf.Lerp(startInner, endInner, curveValue);
            light2D.pointLightOuterRadius = Mathf.Lerp(startOuter, endOuter, curveValue);
            
            if (curveValue >= 0.95f)
            {
                StunEnemy();
                CameraController.Instance.Shake();
            }
            yield return null;
        }
        
        yield return new WaitForSeconds(0.5f);
        
        startTime = Time.time;
        while (startTime + activateDuration >= Time.time)
        {   
            light2D.intensity = Mathf.Lerp(endIntensity, startIntensity, curve.Evaluate(Time.time - startTime / activateDuration));
            light2D.pointLightInnerRadius = Mathf.Lerp(endInner, startInner, curve.Evaluate(Time.time - startTime / activateDuration));
            light2D.pointLightOuterRadius = Mathf.Lerp(endOuter, startOuter, curve.Evaluate(Time.time - startTime / activateDuration));
            yield return null;
        }
    }

    void StunEnemy()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D col in cols)
        {
            if (col.TryGetComponent<IStunable>(out IStunable enemy))
            {
                enemy.Stun();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<ILightable>(out ILightable target))
        {
            target.TakeLight();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<ILightable>(out ILightable target))
        {
            target.StopTakeLight();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
