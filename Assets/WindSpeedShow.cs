using System;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class WindTurbineDisplay : MonoBehaviour
{
    public TextMeshPro textBubbleRPM; // Assign in Inspector
    public TextMeshPro textBubbleDir;
    public TextMeshPro textBubblePower;
    public TextMeshPro textBubbleVisibility;
    public TextMeshPro textBubbleShowers;
    public TextMeshPro Rain;
    public TextMeshPro windSpeed;


    public void SetVisibility(float visibility)
    {
        if (textBubbleVisibility != null)
            textBubbleVisibility.text = $"Visibility: {visibility:0.0} m";
    }
    public void SetRain(float rain)
    {
        if (Rain != null)
            Rain.text = $"Rain: {rain:0.00} mm";
    }

    public void SetShowers(float showers)
    {
        if (textBubbleShowers != null)
            textBubbleShowers.text = $"Showers: {showers:0.00} mm";
    }
    public void SetWindSpeed(double speed)
    {
        
        windSpeed.text = "Wind Speed -> " + (Math.Floor(speed * 100) / 100).ToString() + " m/s";
        windSpeed.color = Color.white ;

    }

    public void SetRpm(double rpm)
    {
        
        textBubbleRPM.text = "Turbine Speed -> " + rpm.ToString() + " rpm";
        textBubbleRPM.color = Color.white;
    }

    public void SetWindPower(double power)
    {
        
        textBubblePower.text = "Power Gen -> " + power.ToString() + " kW/h";
        textBubblePower.color = Color.white;
    }

    public void SetWindowDirection(float direction)
    {
        var compassDirection = DegreesToCompass(direction);
        textBubbleDir.text = "Wind Direction -> " + compassDirection;
        textBubbleDir.color = Color.white;
    }
    public static string DegreesToCompass(float degrees)
    {
        string[] directions = { "N", "NE", "E", "SE", "S", "SW", "W", "NW", "N" };
        int index = Mathf.RoundToInt(degrees / 45f);
        return directions[index % 8];
    }

}