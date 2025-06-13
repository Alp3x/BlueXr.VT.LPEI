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
    public float manualSnow = 0f;


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
    }
    public void SetManualAirDirection(float value)
    {
        manualAirDirection = Mathf.RoundToInt(value);
    }
    public void SetManualVisibility(float value)
    {
        float minVis = 10f;
        float maxVis = 10000f;
        manualVisibility = Mathf.Exp(Mathf.Lerp(Mathf.Log(maxVis), Mathf.Log(minVis), value));
    }
    public void SetManualShowers(float value)
    {
        manualShowers = value;

    }
    public void SetManualRain(float value)
    {
        manualRain = value;
    }

    public void SetManualSnow(float value)
    {
        manualSnow = value;
    }

    // Method for UI toggle
    public void SetAPIMode(bool value)
    {
        useAPIMode = !useAPIMode;
    }
}
