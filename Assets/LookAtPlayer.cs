using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main != null)
        {
            // Make the canvas face the camera
            transform.LookAt(Camera.main.transform);
            // Optionally, reverse to face the camera front, not back
            transform.Rotate(0, 180, 0);
        }
    }
}
