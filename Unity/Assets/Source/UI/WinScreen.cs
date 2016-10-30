using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void Quit()
    {
        SceneManager.LoadScene("start");
    }
}
