using System.Net.Http;
using GLTFast.Schema;
using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Timers;
using UnityEditor.ShaderGraph.Serialization;

public class MeteoAdaptaion : MonoBehaviour
{
    // JSON FILE DATA
    public class Hourly
    {
        public List<string> time { get; set; }
        public List<double> wind_speed_10m { get; set; }
        public List<int> wind_direction_10m { get; set; }
    }

    public class HourlyUnits
    {
        public string time { get; set; }
        public string wind_speed_10m { get; set; }
        public string wind_direction_10m { get; set; }
    }

    public class Root
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public double elevation { get; set; }
        public HourlyUnits hourly_units { get; set; }
        public Hourly hourly { get; set; }
    }
    // ----


    public UnityEngine.Animation anim;
    public string animationName = "idle"; 
    public float newSpeed;
    public float airSpeed;
    private Root forecast;
    public string json;
    private static System.Timers.Timer hourlyTimer;
    void Start()
    {
        requestData(this,null);
        hourlyTimer = new System.Timers.Timer(3600000);
        hourlyTimer.Elapsed += requestData;
        hourlyTimer.AutoReset = true;
        hourlyTimer.Enabled = true;
        forecast = JsonUtility.FromJson<Root>(json);

        // Check if the animation component and clip exist
        if (anim == null && anim.GetClip(animationName) == null)
        {
            Debug.LogWarning("Animation or clip not found!");
        }
        int i = 0;
        while (forecast.hourly.time[i] == DateTime.Now.ToString())
        {
            i++;
        }
        airSpeed = (float)(forecast.hourly.wind_speed_10m[i] * 0.27);

        transform.Rotate(0, 0, forecast.hourly.wind_direction_10m[i]);
        Debug.Log(forecast.hourly.wind_direction_10m[i]);
        anim[animationName].speed = airSpeed;
        
    }

    void requestData(object sender, ElapsedEventArgs e)
    {
        var latitude = 41.375;
        var longitude = -8.75;
        var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=wind_speed_10m,wind_direction_10m";

        using var httpClient = new HttpClient();
        try
        {
            var response = httpClient.GetStringAsync(url);
            json = JsonUtility.ToJson(response.ToString());
            Debug.Log("Response recieved!!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

