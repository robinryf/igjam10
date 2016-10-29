using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void Retry()
    {
        LevelManager.Instance.Retry();
    }

    public void Quit()
    {
        SceneManager.LoadScene("start");
    }
}
