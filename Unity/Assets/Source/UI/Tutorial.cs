using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance { get; private set; }

    public IEnumerator ShowTutorial()
    {
        while (Input.GetKeyUp(KeyCode.Joystick1Button0) == false && Input.GetKeyUp(KeyCode.Return) == false)
        {
            yield return null;
        }
    }
}
