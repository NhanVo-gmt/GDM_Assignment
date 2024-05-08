using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;

    public Vector2 playerCheckPoint;
    public int sceneIndex;

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

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += ResetStartPos;
    }

    private void ResetStartPos(Scene scene, LoadSceneMode arg1)
    {
        if (sceneIndex != scene.buildIndex)
        {
            sceneIndex = scene.buildIndex;
            playerCheckPoint = GameObject.FindWithTag("Player").transform.position;
        }
        else
        {
            GameObject.FindWithTag("Player").transform.position = playerCheckPoint;
        }
    }
    
    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        playerCheckPoint = newCheckpoint;
    }
}
