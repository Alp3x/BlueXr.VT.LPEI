using TMPro;
using UnityEngine;

public class WindTurbineDisplay : MonoBehaviour
{
    public TextMeshPro textBubble; // Assign in Inspector
    public TextMeshPro textBubbleDir;
    public TextMeshPro textBubblePower;
    public void SetWindSpeed(float speed)
    {
        
        textBubble.text = "Turbine Speed -> " + speed.ToString() + " m/s";
        textBubble.color = Color.black ;
    }

    public void SetWindPower(double power)
    {
        
        textBubblePower.text = "Power Gen -> " + power.ToString() + " kW/h";
        textBubblePower.color = Color.black;
    }

    public void SetWindowDirection(float direction)
    {
        var compassDirection = DegreesToCompass(direction);
        textBubbleDir.text = "Wind Direction -> " + compassDirection;
        textBubbleDir.color = Color.black;
    }
    public static string DegreesToCompass(float degrees)
    {
        string[] directions = { "N", "NE", "E", "SE", "S", "SW", "W", "NW", "N" };
        int index = Mathf.RoundToInt(degrees / 45f);
        return directions[index % 8];
    }
}