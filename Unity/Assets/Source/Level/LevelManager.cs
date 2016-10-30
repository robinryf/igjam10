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

    public AudioSource GameOverSound;
    public PlayMusic MusicController;

    private int currentLevelIndex = -1;

    public string[] LevelNames;

    private bool gameStarted;

    private void Awake()
    {
        if (Instance != null)
        {
            Instance.gameStarted = false;
            Instance.currentLevelIndex = -1;
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void StartGame()
    {
        if (gameStarted)
        {
            return;
        }
        gameStarted = true;
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

        if (currentLevelIndex >= LevelNames.Length)
        {
            // reached end of game.
            SceneManager.LoadScene("win");
            return;
        }
        
        var levelName = LevelNames[currentLevelIndex];
        SceneManager.LoadScene(levelName);
    }

    private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode)
    {
        if (arg0.name == "start")
        {
            return;
        }
        gameStarted = true;
        // setup scene
        var player = FindObjectOfType<PlayerMovementController>();
        if (player != null)
        {
            player.Paused = true;
        }
        CodeGenerationRunner.Instance.Disable();
        InitializeText.Instance.gameObject.SetActive(true);

        // start timer
        StartCoroutine(StartSequenceLog());
    }

    private IEnumerator StartSequenceLog()
    {
        for (int i = 0; i < 10; i++)
        {
            string line;
            string middleScreenLine;
            bool warn = false;
            bool error = false;
            switch (i)
            {
                case 0:
                    line = "010%: Initializing console engine...";
                    middleScreenLine = "Initializing: 010%";
                    break;
                case 1:
                    line = "020%: Feeding hamsters...";
                    middleScreenLine = "Initializing: 020%";
                    break;
                case 2:
                    line = "030%: Registering native extension...";
                    middleScreenLine = "Initializing: 030%";
                    break;
                case 3:
                    line = "040%: Deleting COBOL code...";
                    middleScreenLine = "Initializing: 040%";
                    break;
                case 4:
                    line = "050%: Filling query string cache...";
                    middleScreenLine = "Initializing: 050%";
                    break;
                case 5:
                    line = "060%: Hiding eastereggs...";
                    middleScreenLine = "Initializing: 060%";
                    break;
                case 6:
                    line = "070%: Recalibrating power management...";
                    middleScreenLine = "Initializing: 070%";
                    break;
                case 7:
                    line = "080%: Disonnecting guest clients (9/10)...";
                    middleScreenLine = "Initializing: 080%";
                    warn = true;
                    break;
                case 8:
                    line = "090%: Detecting weired smell in system...";
                    middleScreenLine = "Initializing: 090%";
                    warn = true;
                    break;
                case 9:
                    line = "100%: INTRUDER ALERT! ACTIVATING LOCK-IN ROUTINE.";
                    middleScreenLine = "Initializing: 100%";
                    error = true;
                    break;
                default:
                    middleScreenLine = "...i";
                    line = "...";
                    break;
            }

            if (error)
            {
                CodeGenerationRunner.Instance.HistoryUi.PrintError(line);
                InitializeText.Instance.Text.color = Color.red;
            }
            else if (warn)
            {
                CodeGenerationRunner.Instance.HistoryUi.PrintHint(line);
                InitializeText.Instance.Text.color = Color.yellow;
            }
            else
            {
                CodeGenerationRunner.Instance.HistoryUi.PrintSuccess(line);
                InitializeText.Instance.Text.color = Color.green;
            }

            InitializeText.Instance.Text.text = middleScreenLine;

            yield return new WaitForSeconds(0.3f);
        }

        var player = FindObjectOfType<PlayerMovementController>();
        if (player != null)
        {
            player.Paused = false;
        }
        CodeGenerationRunner.Instance.Enable();
        InitializeText.Instance.gameObject.SetActive(false);
    } 

    public void GameOver()
    {
        if (!gameStarted) return;

        gameStarted = false;
        MusicController.FadeDown(0.2F);
        GameOverSound.Play(750);
        var player = FindObjectOfType<PlayerMovementController>();
        if (player != null)
        {
            player.Paused = true;
            player.rigid.velocity = Vector2.zero;
        }
        CodeGenerationRunner.Instance.Disable();
        GameOverScreen.Instance.gameObject.SetActive(true);
    }

    public void Retry()
    {
        gameStarted = false;
        SceneManager.LoadScene(LevelNames[currentLevelIndex]);
    }
}
