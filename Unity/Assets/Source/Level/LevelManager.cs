using System;
using System.Collections;
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
        var player = FindObjectOfType<PlayerMovementController>();
        player.Paused = true;
        CodeGenerationRunner.Instance.Disable();

        // start timer
        StartCoroutine(StartSequenceLog());
    }

    private IEnumerator StartSequenceLog()
    {
        for (int i = 0; i < 10; i++)
        {
            string line;
            switch (i)
            {
                case 0:
                    line = "010%: Initializing console engine...";
                    break;
                case 1:
                    line = "020%: Feeding hamsters...";
                    break;
                case 2:
                    line = "030%: Registering native extension...";
                    break;
                case 3:
                    line = "040%: Deleting COBOL code...";
                    break;
                case 4:
                    line = "050%: Filling query string cache...";
                    break;
                case 5:
                    line = "060%: Hiding eastereggs...";
                    break;
                case 6:
                    line = "070%: Recalibrating power management...";
                    break;
                case 7:
                    line = "080%: Disonnecting guest clients...";
                    break;
                case 8:
                    line = "090%: Modifying response protocol...";
                    break;
                case 9:
                    line = "100%: INTRUDER ALERT! ACTIVATING LOCK-IN ROUTINE.";
                    break;
                default:
                    line = "...";
                    break;
            }

            CodeGenerationRunner.Instance.HistoryUi.PrintSuccess(line);

            yield return new WaitForSeconds(0.3f);
        }

        var player = FindObjectOfType<PlayerMovementController>();
        player.Paused = false;
        CodeGenerationRunner.Instance.Enable();
    } 

    public void GameOver()
    {
        var player = FindObjectOfType<PlayerMovementController>();
        player.Paused = true;
        player.rigid.velocity = Vector2.zero;
        CodeGenerationRunner.Instance.Disable();
        GameOverScreen.Instance.gameObject.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(LevelNames[currentLevelIndex]);
    }
}
