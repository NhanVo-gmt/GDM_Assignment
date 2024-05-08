using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void RestartGame()
    {
        StartCoroutine(RestartGameCoroutine());
    }

    IEnumerator RestartGameCoroutine()
    {
        yield return SceneLoaderUI.Instance.FadeIn();
        SceneManager.LoadScene(0);
        
        yield return SceneLoaderUI.Instance.FadeOut();
    }
    
    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }
    
    IEnumerator StartGameCoroutine()
    {
        yield return SceneLoaderUI.Instance.FadeIn();
        SceneManager.LoadScene(1);
        
        yield return SceneLoaderUI.Instance.FadeOut();
    }

    public void RestartLevel()
    {
        StartCoroutine(RestartLevelCoroutine());
    }
    
    IEnumerator RestartLevelCoroutine()
    {
        yield return SceneLoaderUI.Instance.FadeIn();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        yield return SceneLoaderUI.Instance.FadeOut();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level + 1);
    }
}
