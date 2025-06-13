using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class WeatherParticleController : MonoBehaviour
{
    [Header("Particle Systems")]
    public ParticleSystem visibilityParticleSystem;
    public ParticleSystem rainParticleSystem;
    public ParticleSystem snowParticleSystem;

    [Header("Weather Provider")]
    public MeteoDataProvider meteoProvider; // Assign in Inspector or auto-find

    private void Awake()
    {
        if (meteoProvider == null)
            meteoProvider = FindObjectOfType<MeteoDataProvider>();
    }

    private void Update()
    {
        float visibility = 1000f;
        float rain = 0f;
        float airSpeed = 0f;
        float showers = 0;
        float snow = 0f;

        // Use API or manual values based on WeatherModeManager
        if (WeatherModeManager.Instance != null && !WeatherModeManager.Instance.useAPIMode)
        {
            visibility = WeatherModeManager.Instance.manualVisibility;
            rain = WeatherModeManager.Instance.manualRain;
            airSpeed = WeatherModeManager.Instance.manualAirSpeed;
            showers = WeatherModeManager.Instance.manualShowers;
            snow = WeatherModeManager.Instance.manualSnow;
        }
        if (meteoProvider != null)
        {
            visibility = meteoProvider.Visibility;
            rain = meteoProvider.Rain;
            airSpeed = meteoProvider.AirSpeed;
            showers = meteoProvider.Showers;
            snow = meteoProvider.Snowfall;
        }
        UpdateParticles(visibility, rain, airSpeed,showers,snow);
    }

    public void UpdateParticles(float visibility, float rain, float airSpeed,float showers,float snow)
    {
        if (rainParticleSystem != null)
        {
            var emission = rainParticleSystem.emission;
            var main = rainParticleSystem.main;

            if(rain > 0.1f && rain <= 5f)
            {
                emission.rateOverTime = 5;
                rainParticleSystem.Play();
            }
            else if (rain > 5f && rain <= 10f)
            {
                emission.rateOverTime = 100;
                rainParticleSystem.Play();
            }
            else if (rain > 10f)
            {
                emission.rateOverTime = 250;
                var vol = rainParticleSystem.velocityOverLifetime;
                vol.radial = airSpeed >= 7 ? 0.1f : 0.05f;
                rainParticleSystem.Play();
            }
            else if(showers > 0.01f && showers <= 1f)
            {
                emission.rateOverTime = 5f;
                rainParticleSystem.Play();
            }
            else 
            {
                rainParticleSystem.Stop();
            }
        }

        if (visibilityParticleSystem != null)
        {
            var main = visibilityParticleSystem.main;
            var emission = visibilityParticleSystem.emission;

            if (visibility <= 30)
            {
                main.maxParticles = 5000;
                main.startLifetime = 5f;
                main.startSpeedMultiplier = airSpeed / 10;
                emission.rateOverTime = 500;
                visibilityParticleSystem.Play();
            }
            else if (visibility > 30 && visibility <= 2500)
            {
                main.maxParticles = 5000;
                main.startLifetime = 30f;
                main.startSpeedMultiplier = airSpeed / 100;
                emission.rateOverTime = 50;
                visibilityParticleSystem.Play();
            }
            else if (visibility > 250)
            {
                main.maxParticles = 0;
                emission.rateOverTime = 0;
                visibilityParticleSystem.Play();
            }
        }
        if(snowParticleSystem != null)
        {
            var emission = snowParticleSystem.emission;
            var main = snowParticleSystem.main;
            if (snow > 0.1f && snow <= 5f)
            {
                emission.rateOverTime = 5;
                snowParticleSystem.Play();
            }
            else if (snow > 5f && snow <= 10f)
            {
                emission.rateOverTime = 10;
                snowParticleSystem.Play();
            }
            else if (snow > 10f)
            {
                emission.rateOverTime = 50;
                var vol = snowParticleSystem.velocityOverLifetime;
                vol.radial = airSpeed >= 7 ? 0.1f : 0.05f;
                snowParticleSystem.Play();
            }
            else
            {
                snowParticleSystem.Stop();
            }
        }
    }
}