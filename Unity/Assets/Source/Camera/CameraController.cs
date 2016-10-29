using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Camera Camera;
    public GameObject TrackingObject;

    public Rect ScreenMoveRect;

    private void Update()
    {
        // get player position in view
        var screenPos = Camera.WorldToScreenPoint(TrackingObject.transform.position);

        var normalizedScreenPos = screenPos;
        normalizedScreenPos.x /= Screen.width;
        normalizedScreenPos.y /= Screen.height;

        Debug.Log(string.Format("ScreenPos: {0}", normalizedScreenPos));
        if (ScreenMoveRect.Contains(normalizedScreenPos) == false)
        {
            Debug.Log("Applying move");
            // We are outside the rect.
            var delta = screenPos.x - ScreenMoveRect.x;
            
            var pos = Camera.transform.position;
            pos.x += delta;
            Camera.transform.position = pos;
        }
    }
}
