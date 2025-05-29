using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportOnPress : MonoBehaviour
{
    public Transform xrRig;           // Assign XR Rig here
    public Transform targetPlatform;  // Assign target platform here

    public void TeleportPlayer()
    {
        if (xrRig != null && targetPlatform != null)
        {
            xrRig.position = targetPlatform.position;
        }
    }
}