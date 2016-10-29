using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public static GameOverScreen Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Retry()
    {
        LevelManager.Instance.Retry();
    }

    public void Quit()
    {
        SceneManager.LoadScene("start");
    }
}
