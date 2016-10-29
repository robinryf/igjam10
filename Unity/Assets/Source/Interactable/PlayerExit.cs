using UnityEngine;
using System.Collections;

public class PlayerExit : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if we are colliding with player.
        if (collider.gameObject.tag == "Player")
        {
            if (LevelManager.Instance == null)
            {
                Debug.LogWarning("You are playing the level directly or the level manager was destroyed. Please use the start scene.");
                return;
            }
            // Player has reached us.
            LevelManager.Instance.EndLevel();
        }
    }
}
