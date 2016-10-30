using UnityEngine;
using System.Collections;

public class PickupTime : MonoBehaviour
{
    public AudioSource pickSound;
    public int bonus = 20;
    private bool picked = false;
    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!picked) return;

        if (!pickSound.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != player) return;

        picked = true;
        pickSound.Play();
        TimeHealthBar.Instance.AddTimeHealth(bonus);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
