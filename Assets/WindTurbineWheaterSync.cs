using GLTFast.Schema;
using System;
using UnityEngine;

public class WindTurbineWeatherSync : MonoBehaviour
{
    public MeteoDataProvider meteoProvider; // Assign in Inspector or find at runtime
    public WindTurbineDisplay turbineDisplay; // Assign in Inspector
    public UnityEngine.Animation anim; // Assign in Inspector
    public string animationName = "idle";

    private void Awake()
    {
        if (meteoProvider == null)
            meteoProvider = FindObjectOfType<MeteoDataProvider>();
        if (turbineDisplay == null)
            turbineDisplay = GetComponent<WindTurbineDisplay>();
    }
    System.Random rand;
    double efficiency;
    private void Start()
    {
        rand = new System.Random();
        efficiency = rand.NextDouble() * 0.05 + 0.45;
    }

    private void Update()
    {
        if (turbineDisplay == null)
            return;

        // Decide which values to use
        bool useAPI = WeatherModeManager.Instance == null || WeatherModeManager.Instance.useAPIMode;

        float airSpeed, visibility, showers, rain;
        int airDirection;

        if (useAPI && meteoProvider != null)
        {
            airSpeed = meteoProvider.AirSpeed;
            airDirection = meteoProvider.AirDirection;
            visibility = meteoProvider.Visibility;
            showers = meteoProvider.Showers;
            rain = meteoProvider.Rain;
        }
        else
        {
            var mgr = WeatherModeManager.Instance;
            airSpeed = mgr.manualAirSpeed;
            airDirection = mgr.manualAirDirection;
            visibility = mgr.manualVisibility;
            showers = mgr.manualShowers;
            rain = mgr.manualRain;
        }

        turbineDisplay.SetWindSpeed(airSpeed);
        turbineDisplay.SetWindowDirection(airDirection);
        turbineDisplay.SetVisibility(visibility);
        turbineDisplay.SetRain(rain);
        turbineDisplay.SetShowers(showers);

        const double airDensity = 1.225;
        double v = airSpeed;
        double r = 111;
        double tsr = 7.0;

        double area = Math.PI * Math.Pow(r, 2);
        double power = (0.5 * airDensity * area * Math.Pow(v, 3) * efficiency) / 1000;
        double powerKW = Math.Floor(power * 100) / 100.0;

        double omega = tsr * v / r;
        double rpm = (omega * r) / (2 * Math.PI);
        rpm = Math.Floor(rpm * 100) / 100.0;



        turbineDisplay.SetWindPower(powerKW);
        turbineDisplay.SetRpm(rpm);

        transform.localRotation = Quaternion.Euler(0, airDirection, 0);

        if (anim != null && anim[animationName] != null)
        {
            anim[animationName].speed = airSpeed / 4f; // Adjust divisor as needed for realism
        }
    }
}