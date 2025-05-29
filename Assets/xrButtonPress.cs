using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRButtonLegacyAnimation : MonoBehaviour
{
    public Animation legacyAnimation; // Reference to Animation component
    public string animationName = "ButtonPress"; // Name of your animation clip

    public void PlayLegacyAnimation()
    {
        if (legacyAnimation != null)
        {
            legacyAnimation.Play(animationName);
        }
    }
}