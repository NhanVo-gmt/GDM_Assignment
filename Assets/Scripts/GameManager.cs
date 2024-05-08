using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Vector2 checkpoint;

    private Player player;
    private Menu menu;

    private void Awake()
    {
        Instance = this;
        
        GameObject playerGO = GameObject.FindWithTag("Player");
        player = playerGO.GetComponent<Player>();
        playerGO.GetComponent<PlayerHealth>().OnDie += RestartScene;

        menu = GetComponentInChildren<Menu>();
    }


    private void RestartScene()
    {
        menu.RestartLevel();
    }
    

    public void Sleep(float duration)
    {
        StartCoroutine(PerformSleep(duration));
    }


    IEnumerator PerformSleep(float duration)
    {
        yield return new WaitForSeconds(0.01f);
            
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        
    }
}
