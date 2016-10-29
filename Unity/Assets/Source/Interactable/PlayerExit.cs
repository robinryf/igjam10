using UnityEngine;
using System.Collections;

public class PlayerExit : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if we are colliding with player.
        if (collider.gameObject.tag == "Player")
        {
            // Player has reached us.
            LevelManager.Instance.EndLevel();
        }
    }
}
