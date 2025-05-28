using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class DisplayAirSpeed : MonoBehaviour
{

    public TextMeshProUGUI tmpTextComponent;

    public float airSpeed;
    public MeteoAdaptaion meteoScript;
    void Update()
    {
        var windTurbine = GameObject.Find("windTurbine").GetComponent<MeteoAdaptaion>();
        Debug.Log(tmpTextComponent.text);
        Debug.Log(windTurbine.airSpeed);
    }
}