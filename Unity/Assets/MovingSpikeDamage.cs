using UnityEngine;
using System.Collections;

public class MovingSpikeDamage : MonoBehaviour {

	public int damage = 20;
	TimeHealthBar timeHealthBar;

	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		timeHealthBar = player.GetComponent<TimeHealthBar>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Entered trigger");
		if (other.tag == "Player") {
			timeHealthBar.RemoveTimeHealth(damage);
		}
	}
}
