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
    
    [Header("Light component")]
    private float startInner = 0f;
    private float startOuter = 5f;
    private float startIntensity = 2f;

    private float endInner = 5f;
    private float endOuter = 10f;
    private float endIntensity = 5f;
    
    public float activateDuration;

    private Light2D light;

    private void Awake()
    {
        light = GetComponentInChildren<Light2D>();
    }

    void Update()
    {
        Move();
        Activate();
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
        if (coolDownElapsed + cooldown > Time.time) return;
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            coolDownElapsed = Time.time;
            StartCoroutine(ToggleLightCoroutine());
        }
    }

    IEnumerator ToggleLightCoroutine()
    {
        float startTime = Time.time;
        while (startTime + activateDuration >= Time.time)
        {
            float curveValue = curve.Evaluate(Time.time - startTime / activateDuration);
            light.intensity = Mathf.Lerp(startIntensity, endIntensity, curveValue);
            light.pointLightInnerRadius = Mathf.Lerp(startInner, endInner, curveValue);
            light.pointLightOuterRadius = Mathf.Lerp(startOuter, endOuter, curveValue);
            
            if (curveValue >= 0.95f)
            {
                CameraController.Instance.Shake();
            }
            yield return null;
        }
        
        yield return new WaitForSeconds(0.5f);
        
        startTime = Time.time;
        while (startTime + activateDuration >= Time.time)
        {   
            light.intensity = Mathf.Lerp(endIntensity, startIntensity, curve.Evaluate(Time.time - startTime / activateDuration));
            light.pointLightInnerRadius = Mathf.Lerp(endInner, startInner, curve.Evaluate(Time.time - startTime / activateDuration));
            light.pointLightOuterRadius = Mathf.Lerp(endOuter, startOuter, curve.Evaluate(Time.time - startTime / activateDuration));
            yield return null;
        }
    }
}
