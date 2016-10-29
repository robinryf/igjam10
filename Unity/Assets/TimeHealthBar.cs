using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeHealthBar : MonoBehaviour {

	public int startingTimeHealth = 100;
	public int currentTimeHealth;
	public Slider timeHealthSlider;
	public Image sliderFillImage;
	public Color damagedColor = new Color (1f, 1f, 0);
	public Color healColor = new Color (0, 0, 1f);
	public Color normalColor = new Color(0, 1f, 0);
	public Color criticalColor = new Color(1f, 0, 0);
	public int criticalThreshold = 30;
	public float flashSpeed = 5f;
	bool damaged = false;
	bool healing = false;


	// Use this for initialization
	void Start () {
		timeHealthSlider.wholeNumbers = true;
		timeHealthSlider.minValue = 0f;
		timeHealthSlider.maxValue = startingTimeHealth;

		sliderFillImage.color = normalColor;
		currentTimeHealth = startingTimeHealth;
		timeHealthSlider.value = currentTimeHealth;
	}

	// Update is called once per frame
	void Update () {
		if (damaged) {
			TransitionSliderBackgroundColor (damagedColor);
		} else if (healing) {
			TransitionSliderBackgroundColor (healColor);
		} else {
			ShowStableColor ();
		}
	}

	void TransitionSliderBackgroundColor(Color color) {
		sliderFillImage.color = Color.Lerp (sliderFillImage.color, color, flashSpeed * Time.deltaTime);
	}

	void ShowStableColor() {
		if (currentTimeHealth < criticalThreshold) {
			TransitionSliderBackgroundColor (criticalColor);
		} else {
			TransitionSliderBackgroundColor (normalColor);
		}
	}

	public void AddTimeHealth(int amount) {
		healing = true;
		ModifyTimeHealth (amount);
		healing = false;
	}

	public void RemoveTimeHealth(int amount) {
		damaged = true;
		ModifyTimeHealth(-amount);
		if (currentTimeHealth <= 0) {
			Death ();
		}
		damaged = false;
	}

	void ModifyTimeHealth(int amount) {
		currentTimeHealth += amount;
		timeHealthSlider.value = currentTimeHealth;
	}

	void Death() {
		Debug.Log ("Oh no, you are dead!");
	}
}
