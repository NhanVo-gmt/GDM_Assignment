using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Sign : MonoBehaviour
{
    [SerializeField] private TextMeshPro[] texts;
    
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private GameObject textGO;
    [SerializeField] private Light2D light;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        foreach (TextMeshPro text in texts)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleSign(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleSign(false);
        }
    }

    void ToggleSign(bool isEnable)
    {
        float targetAlpha = isEnable ? 1 : 0;
        if (textGO) textGO.SetActive(isEnable);

        foreach (TextMeshPro text in texts)
        {
            StartCoroutine(ToggleCoroutine(text, targetAlpha));
        }
    }
    
    IEnumerator ToggleCoroutine(TextMeshPro text, float targetAlpha)
    {
        float time = Time.time;
        Color startColor = text.color;
        float startIntensity = 1 - targetAlpha;
        while (time + duration > Time.time)
        {
            float alpha = Mathf.Lerp(startColor.a, targetAlpha, (Time.time - time) / duration);
            text.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            light.intensity = Mathf.Lerp(startIntensity, targetAlpha, (Time.time - time) / duration);
            yield return null;
        }
        
        text.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        light.intensity = targetAlpha;
    }
}
