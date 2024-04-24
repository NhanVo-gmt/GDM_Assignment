using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private GameObject singleImage;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private CanvasGroup loseScreen;

    private List<GameObject> singleImageList = new List<GameObject>();
    
    private void Awake()
    {
        health.OnTakeDamage += UpdateHealthUI;
        SpawnHealthUI(health.maxHealth);
    }

    void SpawnHealthUI(int maxHealth)
    {
        for (int i = 0; i < maxHealth; i++)
        {
            singleImageList.Add(Instantiate(singleImage, transform)); 
        }
    }

    private void UpdateHealthUI(int newHealth)
    {
        for (int i = 0; i < newHealth; i++)
        {
            singleImageList[i].SetActive(true);
        }
        
        for (int i = newHealth; i < singleImageList.Count; i++)
        {
            singleImageList[i].SetActive(false);
        }
        
        if (newHealth <= 0) OpenLoseScreen();
    }

    private void OpenLoseScreen()
    {
        loseScreen.alpha = 1;
        loseScreen.interactable = true;
        loseScreen.blocksRaycasts = true;
    }
}
