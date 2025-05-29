using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; // For TextMeshPro

public class MeteoAdaptation : MonoBehaviour
{
    [Serializable]
    public class Hourly
    {
        public List<string> time;
        public List<float> wind_speed_10m;
        public List<int> wind_direction_10m;
    }

    [Serializable]
    public class HourlyUnits
    {
        public string time;
        public string wind_speed_10m;
        public string wind_direction_10m;
    }

    [Serializable]
    public class Root
    {
        public float latitude;
        public float longitude;
        public float generationtime_ms;
        public int utc_offset_seconds;
        public string timezone;
        public string timezone_abbreviation;
        public float elevation;
        public HourlyUnits hourly_units;
        public Hourly hourly;
    }

    public WindTurbineDisplay turbineDisplay; // Assign in Inspector
    public Animation anim; // Assign in Inspector
    public string animationName = "idle";
    private Root forecast;
    public float airSpeed;

    private void Start()
    {
        StartCoroutine(RequestDataRoutine());
        // Optionally, repeat every hour:
        InvokeRepeating(nameof(RefreshData), 3600f, 3600f); // every hour
    }

    private void RefreshData()
    {
        StartCoroutine(RequestDataRoutine());
    }

    IEnumerator RequestDataRoutine()
    {
        var latitude = 41.375;
        var longitude = -8.75;
        var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=wind_speed_10m,wind_direction_10m";

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Parse JSON (using Unity's JsonUtility, structure must match JSON exactly)
                forecast = JsonUtility.FromJson<Root>(www.downloadHandler.text);

                // Find the closest time index
                int i = 0;
                string nowHour = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:00");
                for (; i < forecast.hourly.time.Count; i++)
                {
                    if (forecast.hourly.time[i].StartsWith(nowHour))
                        break;
                }

                airSpeed = forecast.hourly.wind_speed_10m[i];
                anim[animationName].speed = airSpeed/4;
                turbineDisplay.SetWindSpeed(airSpeed);
                turbineDisplay.SetWindowDirection(forecast.hourly.wind_direction_10m[i]);

                double r = 30;
                double v = airSpeed;
                double rawPowerKW = 0.0006f * Math.Pow(r, 2) * Math.Pow(v, 3);
                double powerKW = Math.Floor(rawPowerKW * 100) / 100;

                turbineDisplay.SetWindPower(powerKW);

                transform.localRotation = Quaternion.Euler(0, forecast.hourly.wind_direction_10m[i], 0);
            }
            else
            {
                Debug.LogError("API request failed: " + www.error);
            }
        }
    }
    Vector3 DegreeToDirection(float deg)
    {
        float radians = deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radians), 0, Mathf.Cos(radians)).normalized;
    }
}