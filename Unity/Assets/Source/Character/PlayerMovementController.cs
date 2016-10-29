using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour
{
    public int DashCount;

    private bool dash;
    public float maxSpeed = 5f;

    private bool moving;
    private Rigidbody2D rigid;
    public float DashDistance;
    public bool Paused;

    public float DashCooldownTime = 3f;

    public int DashLimit = 1;

    private float dashCooldown;

    // Use this for initialization
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Paused)
        {
            return;
        }

        if (DashCount < DashLimit)
        {
            dashCooldown -= Time.deltaTime;
        }

        if (dashCooldown <= 0)
        {
            dashCooldown = DashCooldownTime;
            DashCount++;
            Debug.Log("Got dash;");
        }

        moving = rigid.velocity.Equals(Vector2.zero) == false;
        
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && moving && DashCount > 0)
        {
            DashCount--;
            dash = true;
        }
    }

    void FixedUpdate()
    {
        if (Paused)
        {
            return;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        var input = new Vector2(h, v) * maxSpeed;

        rigid.AddForce(input);

        if (dash)
        {
            var dashVector = rigid.velocity * DashDistance;
            rigid.AddForce(dashVector);
            dash = false;
        }
    }
}
