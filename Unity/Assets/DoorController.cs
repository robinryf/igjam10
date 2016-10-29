using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {
	private GameObject left;
	private GameObject right;
	private Quaternion rotation;
	private bool opened;

	void Start () {
		rotation = gameObject.transform.rotation;
		opened = false;
		foreach (Transform childTransform in gameObject.transform) {
			GameObject child = childTransform.gameObject;
			if (child.name == "left") {
				left = child;
			} else if (child.name == "right") {
				right = child;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Open ();
	}

	public void Open () {
		if (!opened) {
			Vector2 baseLine = new Vector2(1, 0);
			Vector2 direction = (rotation * baseLine).normalized;
			Vector2 leftEndPosition = (Vector2)left.transform.position - direction;
			Vector2 rightEndPosition = (Vector2)right.transform.position + direction;
			StartCoroutine(OpenDoors (leftEndPosition, rightEndPosition, 2));
			opened = true;
		}
	}

	private IEnumerator OpenDoors(Vector2 leftEndPosition, Vector2 rightEndPosition, float time) {
		float elapsedTime = 0;
		while (elapsedTime < time) {
			left.transform.position = Vector2.Lerp (left.transform.position, leftEndPosition, elapsedTime / time);
			right.transform.position = Vector2.Lerp (right.transform.position, rightEndPosition, elapsedTime / time);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
}
