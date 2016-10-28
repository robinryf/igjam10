using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour
{
    private bool dash;
    public float maxSpeed = 5f;

    private bool moving;
    private Rigidbody2D rigid;
    public float DashDistance;


    // Use this for initialization
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moving = rigid.velocity.Equals(Vector2.zero) == false;
        
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && moving)
        {
            dash = true;
        }
    }

    void FixedUpdate()
    {
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
