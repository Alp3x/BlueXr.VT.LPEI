using System;
using UnityEngine;

public class MeteoDataProvider : MonoBehaviour
{
    [Header("References")]
    public MeteoAPIClient apiClient;

    // Latest parsed data
    public MeteoDataModel.Root CurrentData { get; private set; }

    // Convenience properties for current hour
    public float AirSpeed { get; private set; }
    public int AirDirection { get; private set; }
    public float Visibility { get; private set; }
    public float Showers { get; private set; }
    public float Rain { get; private set; }

    private void Awake()
    {
        if (apiClient == null)
            apiClient = FindObjectOfType<MeteoAPIClient>();
    }

    private void OnEnable()
    {
        if (apiClient != null)
            apiClient.OnApiResponse += HandleApiResponse;
    }

    private void OnDisable()
    {
        if (apiClient != null)
            apiClient.OnApiResponse -= HandleApiResponse;
    }

    private void HandleApiResponse(string json)
    {
        var root = JsonUtility.FromJson<MeteoDataModel.Root>(json);
        CurrentData = root;

        // Find the closest hour index
        int i = 0;
        string nowHour = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:00");
        for (; i < root.hourly.time.Count; i++)
        {
            if (root.hourly.time[i].StartsWith(nowHour))
                break;
        }

        AirSpeed = root.hourly.wind_speed_10m[i];
        AirDirection = root.hourly.wind_direction_10m[i];
        Visibility = root.hourly.visibility[i];
        Showers = root.hourly.showers[i];
        Rain = root.hourly.rain[i];

    }
}