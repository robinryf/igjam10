using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeHealthBar : MonoBehaviour {

	public float startingTimeHealth = 100;
	public Slider timeHealthSlider;
	public Image sliderFillImage;
	public Color damagedColor = new Color (1f, 1f, 0);
	public Color healColor = new Color (0, 0, 1f);
	public Color normalColor = new Color(0, 1f, 0);
	public Color criticalColor = new Color(1f, 0, 0);
    public Text TimeDigitsText;
	public float criticalThreshold = 30;
	public float flashSpeed;
	public float dyingRate = 1;
    public float AddTimeOnCorrectCode = 20;
	bool damaged = false;
	bool healing = false;

    private float timeLeft;

    public static TimeHealthBar Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

	void Start ()
	{
        CodeGenerationRunner.Instance.CorrectEvent += OnCorrectCodeEntred;
	    timeLeft = startingTimeHealth;
		timeHealthSlider.minValue = 0f;
		timeHealthSlider.maxValue = startingTimeHealth;
		sliderFillImage.color = normalColor;
		timeHealthSlider.value = startingTimeHealth;
	}

    private void OnCorrectCodeEntred(string s, CodeGenerator.DifficultyType difficulty)
    {
        AddTimeHealth(AddTimeOnCorrectCode);
    }

    private void OnDestroy()
    {
        CodeGenerationRunner.Instance.CorrectEvent -= OnCorrectCodeEntred;
    }
		
	void Update () {
		if (damaged) {
			sliderFillImage.color = damagedColor;
		} else if (healing) {
			sliderFillImage.color = healColor;
		} else {
			ShowStableColor ();
		}
		DecrementTimeHealth ();
	}

    private void DrawTime()
    {
        if (timeLeft < 0)
        {
            timeLeft = 0;
        }
        timeHealthSlider.value = timeLeft;
        var ts = TimeSpan.FromSeconds(timeLeft);
        TimeDigitsText.text = string.Format("{0}:{1}", ts.TotalSeconds, ts.Milliseconds);
    }

	void ShowStableColor() {
		if (timeLeft < criticalThreshold) {
			TransitionSliderBackgroundColor (criticalColor);
		} else {
			TransitionSliderBackgroundColor (normalColor);
		}
	}

	void TransitionSliderBackgroundColor(Color color) {
		sliderFillImage.color = Color.Lerp (sliderFillImage.color, color, flashSpeed * Time.deltaTime);
	}

	public void AddTimeHealth(float amount) {
		healing = true;
		ModifyTimeHealth (amount);
		healing = false;
	}

	public void RemoveTimeHealth(float amount) {
		damaged = true;
		ModifyTimeHealth(-amount);
		CheckIfDead ();
		damaged = false;
	}

	void ModifyTimeHealth(float amount)
	{
	    timeLeft += amount;
        DrawTime();
	}

	void CheckIfDead() {
		if (timeLeft <= 0) {
			Death ();
		}
	}

	void Death()
	{
	    LevelManager.Instance.GameOver();
	}

	void DecrementTimeHealth() {
		ModifyTimeHealth(dyingRate * Time.deltaTime * (-1));
		CheckIfDead ();
	}
}
