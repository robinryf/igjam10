using UnityEngine;
using System.Collections;

public class PlayerSpikeController : MonoBehaviour
{
    public AudioSource bouncingSound;
    public AudioSource hittingSound;

	private Rigidbody2D rigid;
	public float force = 200;
	public float maxSpeed = 500;

	void Start () {
		rigid = gameObject.GetComponent<Rigidbody2D> ();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Spikes" && other.transform.parent.gameObject.tag != "Spikes") return;

        hittingSound.loop = true;
        hittingSound.Play();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != "Spikes" && other.transform.parent.gameObject.tag != "Spikes") return;

        hittingSound.loop = false;
    }

    void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Spikes" || other.transform.parent.gameObject.tag == "Spikes") {
			Vector2 newPlayerVelocity = other.relativeVelocity * -1 * force;
			if (newPlayerVelocity.magnitude > maxSpeed) {
				newPlayerVelocity = Vector2.ClampMagnitude (newPlayerVelocity, maxSpeed);
			}
			rigid.AddForce(newPlayerVelocity);
            bouncingSound.Play();
		}
	}

// Below is code for removing health with trigger instead of bouncing back using collider.

//	void OnTriggerEnter2D(Collision2D other) {
//		if (other.tag == "Spikes" || other.transform.parent.gameObject.tag == "Spikes") {
//			Debug.Log ("knocking player back");
//			RemoveHealth (20);
//		}
//	}

//	void RemoveHealth(int damage) {
//		TimeHealthBar timeHealthBar = gameObject.GetComponent<TimeHealthBar>();
//		timeHealthBar.RemoveTimeHealth(damage);
//	}
}
