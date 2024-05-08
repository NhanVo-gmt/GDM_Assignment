using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderUI : MonoBehaviour
{
    
    public static SceneLoaderUI Instance;

    private CanvasGroup canvasGroup;
    private Coroutine LoadCoroutine;

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

        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    public void StartFade()
    {
        StartCoroutine(FadeIn());
    }
    
    public IEnumerator FadeIn()
    {
        yield return Fade(1, 1);
    }

    public IEnumerator FadeOut()
    {
        if (LoadCoroutine != null)
        {
            yield return LoadCoroutine;
        }
        
        yield return Fade(0, 1);
    }

    IEnumerator Fade(float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;    
    }
}
