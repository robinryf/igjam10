using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public Vector2 startPosition;
	public Vector2 endPosition;
	public float speed;

	// Use this for initialization
	void Start () {
		gameObject.transform.position = startPosition;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = Vector2.MoveTowards (transform.position, endPosition, speed * Time.deltaTime);
		if ((Vector2) gameObject.transform.position == endPosition) {
			Vector2 oldEndPosition = endPosition;
			endPosition = startPosition;
			startPosition = oldEndPosition;
		}
	}
}
