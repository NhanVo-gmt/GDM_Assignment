using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void RestartGame()
    {
        SceneLoader.Instance.RestartGame();
    }

    
    public void StartGame()
    {
        SceneLoader.Instance.StartGame();
    }
    


    public void RestartLevel()
    {
        SceneLoader.Instance.RestartLevel();
    }
    


    public void Exit()
    {
        Application.Quit();
    }

    public void LoadLevel(int level)
    {
        SceneLoader.Instance.LoadLevel(level);
    }
}
