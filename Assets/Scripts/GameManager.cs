using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
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
