using UnityEngine;
using System.Collections;

public class PlayerSpikeController : MonoBehaviour {

	public int damage = 20;
	TimeHealthBar timeHealthBar;

	void Start () {
		timeHealthBar = gameObject.GetComponent<TimeHealthBar>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Spikes" || other.transform.parent.gameObject.tag == "Spikes")
		{
			timeHealthBar.RemoveTimeHealth(damage);
		}
	}
}
