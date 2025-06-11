#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class LightingSettingsSwitcher : MonoBehaviour
{
    public LightingSettings[] settings; // Assign your .lighting assets in the Inspector

    public void ApplyLightingSettings(int index)
    {
        if (settings == null || index < 0 || index >= settings.Length) return;
        Lightmapping.lightingSettings = settings[index];
        Debug.Log($"Applied LightingSettings: {settings[index].name}");
    }
}
#endif
