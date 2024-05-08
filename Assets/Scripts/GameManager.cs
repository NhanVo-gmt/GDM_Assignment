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

    private bool isPaused = false;

    [SerializeField] private CanvasGroup settingHolder;

    private void Awake()
    {
        Instance = this;
        
        GameObject playerGO = GameObject.FindWithTag("Player");
        player = playerGO.GetComponent<Player>();
        playerGO.GetComponent<PlayerHealth>().OnDie += RestartScene;

        menu = GetComponentInChildren<Menu>();
    }

    public void Pause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            settingHolder.interactable = true;
            settingHolder.blocksRaycasts = true;
            settingHolder.alpha = 1;
            Time.timeScale = 0f;
        }
        else
        {
            settingHolder.interactable = false;
            settingHolder.blocksRaycasts = false;
            settingHolder.alpha = 0;
            Time.timeScale = 1f;
        }
    }

    public void Quit()
    {
        menu.RestartGame();
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
