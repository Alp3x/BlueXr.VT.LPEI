using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main != null)
        {
            // Make the canvas face the camera
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }
    }
}
