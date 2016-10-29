using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private int currentLevelIndex = -1;

    public string[] LevelNames;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void StartGame()
    {
        SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
        LoadNextLevel();
    }

    public void EndLevel()
    {
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        currentLevelIndex++;
        var levelName = LevelNames[currentLevelIndex];
        SceneManager.LoadScene(levelName);
    }

    private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode)
    {
        // setup scene
        Debug.Log("Loaded scenes");
        
    }

    public void GameOver()
    {
        var player = FindObjectOfType<PlayerMovementController>();
        player.Paused = true;
        player.rigid.velocity = Vector2.zero;
        GameOverScreen.Instance.gameObject.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(LevelNames[currentLevelIndex]);
    }
}
