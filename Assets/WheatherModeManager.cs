using UnityEngine;

public class WeatherModeManager : MonoBehaviour
{
    public static WeatherModeManager Instance { get; private set; }

    [Header("Mode")]
    public bool useAPIMode = true;

    [Header("Manual Weather Values")]
    public float manualAirSpeed = 0f;
    public int manualAirDirection = 0;
    public float manualVisibility = 10000f;
    public float manualShowers = 0f;
    public float manualRain = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    // Methods for UI to set manual values
    public void SetManualAirSpeed(float value)
    {
        manualAirSpeed = value;
        Debug.Log($"Manual Air Speed set to {manualAirSpeed} m/s");
    }
    public void SetManualAirDirection(float value)
    {
        manualAirDirection = Mathf.RoundToInt(value);
        Debug.Log($"Manual Air Direction set to {manualAirDirection} degrees");
    }
    public void SetManualVisibility(float value)
    {
        float minVis = 10f;
        float maxVis = 10000f;
        manualVisibility = Mathf.Exp(Mathf.Lerp(Mathf.Log(maxVis), Mathf.Log(minVis), value));
        Debug.Log($"Manual Visibility set to {manualVisibility} m (slider value: {value})");
    }
    public void SetManualShowers(float value)
    {
        manualShowers = value;
        Debug.Log($"Manual Showers set to {manualShowers} mm");
    }
    public void SetManualRain(float value)
    {
        manualRain = value;
        Debug.Log($"Manual Rain set to {manualRain} mm");
    }

    // Method for UI toggle
    public void SetAPIMode(bool value)
    {
        useAPIMode = !useAPIMode;
        Debug.Log($"Weather Mode set to {(useAPIMode ? "API" : "Manual")}");
        Debug.Log(useAPIMode);
    }
}
