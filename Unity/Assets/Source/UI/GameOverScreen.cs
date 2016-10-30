using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public static GameOverScreen Instance { get; private set; }
	public InputField input;

    private void Awake()
    {
        Instance = this;
		input.onEndEdit.AddListener(SubmitName);
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

	private void SubmitName(string arg0)
	{
		if (arg0.ToLower () == "restart") {
			Retry ();
		} else if (arg0.ToLower () == "quit") {
			Quit ();
		}
		input.text = "";
	}
}
