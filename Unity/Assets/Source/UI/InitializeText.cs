using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InitializeText : MonoBehaviour
{
    public Text Text;
    public static InitializeText Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SetText(string t)
    {
        Text.text = t;
    }
}
