﻿using UnityEngine;
using System.Collections;

public class SpikeDamage : MonoBehaviour {

	public int damage = 20;
	TimeHealthBar timeHealthBar;
	GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		timeHealthBar = player.GetComponent<TimeHealthBar>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject == player)
		{
			timeHealthBar.RemoveTimeHealth(damage);
		}
	}
}
