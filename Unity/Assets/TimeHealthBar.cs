using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeHealthBar : MonoBehaviour {

	public int startingTimeHealth = 100;
	//public int currentTimeHealth;
	public Slider timeHealthSlider;
	public Image sliderFillImage;
	public Color damagedColor = new Color (1f, 1f, 0);
	public Color healColor = new Color (0, 0, 1f);
	public Color normalColor = new Color(0, 1f, 0);
	public Color criticalColor = new Color(1f, 0, 0);
	public int criticalThreshold = 30;
	public float flashSpeed;
	public int dyingRate = 1;
	bool damaged = false;
	bool healing = false;


	// Use this for initialization
	void Start () {
		timeHealthSlider.minValue = 0f;
		timeHealthSlider.maxValue = startingTimeHealth;
		sliderFillImage.color = normalColor;
		timeHealthSlider.value = startingTimeHealth;
	}

	// Update is called once per frame
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

	void ShowStableColor() {
		if (timeHealthSlider.value < criticalThreshold) {
			TransitionSliderBackgroundColor (criticalColor);
		} else {
			TransitionSliderBackgroundColor (normalColor);
		}
	}

	void TransitionSliderBackgroundColor(Color color) {
		sliderFillImage.color = Color.Lerp (sliderFillImage.color, color, flashSpeed * Time.deltaTime);
	}

	public void AddTimeHealth(int amount) {
		healing = true;
		ModifyTimeHealth (amount);
		healing = false;
	}

	public void RemoveTimeHealth(int amount) {
		damaged = true;
		ModifyTimeHealth(-amount);
		CheckIfDead ();
		damaged = false;
	}

	void ModifyTimeHealth(int amount) {
		timeHealthSlider.value += amount;
	}

	void CheckIfDead() {
		if (timeHealthSlider.value <= 0) {
			Death ();
		}
	}

	void Death() {
		Debug.Log ("Oh no, you are dead!");
	}

	void DecrementTimeHealth() {
		timeHealthSlider.value -= dyingRate * Time.deltaTime;
		CheckIfDead ();	
	}
}
