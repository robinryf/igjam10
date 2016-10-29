using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public Vector2 delta;
	public float speed;
    private Vector2 savedOrigin;
    private Vector2 savedTarget;

	// Use this for initialization
	void Start ()
	{
	    savedOrigin = transform.position;
	    savedTarget = savedOrigin + delta;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = Vector2.MoveTowards (transform.position, savedTarget, speed * Time.deltaTime);
		if ((Vector2) gameObject.transform.position == savedTarget) {
			Vector2 oldEndPosition = savedTarget;
			savedTarget = savedOrigin;
			savedOrigin = oldEndPosition;
		}
	}
}
