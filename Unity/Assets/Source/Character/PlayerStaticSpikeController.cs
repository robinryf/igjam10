using UnityEngine;
using System.Collections;

public class PlayerStaticSpikeController : MonoBehaviour {

	public int damage = 20;
	TimeHealthBar timeHealthBar;

	void Start () {
		timeHealthBar = gameObject.GetComponent<TimeHealthBar>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.transform.parent.gameObject.tag.Contains("Spike"))
		{
			timeHealthBar.RemoveTimeHealth(damage);
		}
	}
}
