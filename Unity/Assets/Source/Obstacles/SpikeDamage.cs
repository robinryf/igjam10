using UnityEngine;
using System.Collections;

public class SpikeDamage : MonoBehaviour {

	TimeHealthBar timeHealthBar;
	GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		timeHealthBar = player.GetComponent<TimeHealthBar>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Trigger entered");
		Debug.Log(other.gameObject.layer);
		if (other.gameObject == player)
		{
			timeHealthBar.RemoveTimeHealth(20);
		}
	}
}
