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
        StartCoroutine(LoadLevelCoroutine(0));
    }

    
    public void StartGame()
    {
        StartCoroutine(LoadLevelCoroutine(1));
    }
    
    public void RestartLevel()
    {
        StartCoroutine(LoadLevelCoroutine(SceneManager.GetActiveScene().buildIndex));
    }
    
    IEnumerator LoadLevelCoroutine(int buildIndex)
    {
        yield return SceneLoaderUI.Instance.FadeIn();
        SceneManager.LoadScene(buildIndex);
        
        yield return SceneLoaderUI.Instance.FadeOut();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadLevel(int level)
    {
        StartCoroutine(LoadLevelCoroutine(level+1));
    }
}
