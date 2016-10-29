using UnityEngine;
using System.Collections;

public class PickupTime : MonoBehaviour {

	public int bonus = 50;
	TimeHealthBar timeHealthBar;
	GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		timeHealthBar = player.GetComponent<TimeHealthBar>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject == player) {
			timeHealthBar.AddTimeHealth(bonus);
			gameObject.active = false;
		}
	}
}
