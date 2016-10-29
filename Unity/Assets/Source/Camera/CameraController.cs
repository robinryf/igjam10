using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Camera Camera;
    public PlayerMovementController Player;

    public Rect ViewPortRect;


    public Vector2 ViewPortPercentage;

    public float CameraMoveTime;

    public float CameraMoveDistance;

    private float cameraTimer;
    private float cameraStartMove;

    private bool moving;

    private void Update()
    {
        if (moving)
        {
            cameraTimer -= Time.deltaTime;
            if (cameraTimer <= 0)
            {
                cameraTimer = 0;
                moving = false;
                Player.Paused = false;
            }

            var cameraPos = Mathf.Lerp(cameraStartMove, cameraStartMove + CameraMoveDistance, 1 - (cameraTimer / CameraMoveTime));
            var pos = Camera.transform.position;
            pos.x = cameraPos;
            Camera.transform.position = pos;
            return;
        }

        var origin = Camera.ViewportToWorldPoint(Vector3.zero);
        var max = Camera.ViewportToWorldPoint(Vector3.one);

        var worldRect = new Rect(origin.x, origin.y, max.x - origin.x, max.y - origin.y);

        var constrainedRect = worldRect;

        //constrainedRect.xMin *= ViewPortPercentage.x;
        constrainedRect.xMax *= ViewPortPercentage.x;

        DrawRect(constrainedRect, Color.green);

        var trackingPos = Player.transform.position;
        ;
        if (constrainedRect.Contains(trackingPos) == false)
        {
            Debug.Log("Detect out of bounds");
            Player.Paused = true;
            moving = true;
            cameraTimer = CameraMoveTime;
            cameraStartMove = Camera.transform.position.x;
        }
    }

    private void DrawRect(Rect rect, Color color)
    {
        Debug.DrawLine(new Vector2(rect.xMin, rect.yMin), new Vector3(rect.xMax, rect.yMin), color);
        Debug.DrawLine(new Vector2(rect.xMax, rect.yMin), new Vector3(rect.xMax, rect.yMax), color);
        Debug.DrawLine(new Vector2(rect.xMax, rect.yMax), new Vector3(rect.xMin, rect.yMax), color);
        Debug.DrawLine(new Vector2(rect.xMin, rect.yMax), new Vector3(rect.xMin, rect.yMin), color);
    }
}
