using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    public float FadeTime = 2;
    public Image FadeImage;

    public CanvasGroup MainMenuGroup;

    private float currentFadeTime;

    private void Start()
    {
        currentFadeTime = FadeTime;
    }

    private void Update()
    {
        currentFadeTime -= Time.deltaTime;

        var col = FadeImage.color;
        col.a = Mathf.Lerp(1, 0, 1 - (currentFadeTime / FadeTime));
        FadeImage.color = col;
        var groupAlpha = Mathf.Lerp(0, 1, 1 - (currentFadeTime / FadeTime));
        MainMenuGroup.alpha = groupAlpha;

        if (currentFadeTime <= 0)
        {
            enabled = false;
        }
    }
}
