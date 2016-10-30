using UnityEngine;
using System.Collections;

public class PickupTime : MonoBehaviour
{

    public int bonus = 20;
    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != player) return;

        TimeHealthBar.Instance.AddTimeHealth(bonus);
        gameObject.SetActive(false);
    }
}
