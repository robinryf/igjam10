using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance { get; private set; }

    public Image TutorialImage;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator ShowTutorial()
    {
        TutorialImage.enabled = true;
        while (Input.GetKeyUp(KeyCode.Joystick1Button0) == false && Input.GetKeyUp(KeyCode.Return) == false)
        {
            yield return null;
        }
        TutorialImage.enabled = false;
    }
}
