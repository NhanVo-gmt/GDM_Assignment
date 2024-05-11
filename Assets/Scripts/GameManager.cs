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
    private bool isPhase2 = false;

    [SerializeField] private CanvasGroup settingHolder;
    [SerializeField] private BookUI bookUI;

    [SerializeField] private Boss boss;
    [SerializeField] private Transform positionPhase2;
    

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

    public void PickupBook(string text)
    {
        bookUI.SetText(text);
        bookUI.Toggle(true);
    }

    public void Phase2()
    {
        if (isPhase2) return;
        
        isPhase2 = true;
        CameraController.Instance.ShakeStrong();
        boss.Phase2();
        boss.transform.position = positionPhase2.position;
    }
}
