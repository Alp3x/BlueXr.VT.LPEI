    using UnityEngine;

public class WeatherParticleController : MonoBehaviour
{
    [Header("Particle Systems")]
    public ParticleSystem visibilityParticleSystem;
    public ParticleSystem rainParticleSystem;

    [Header("Weather Provider")]
    public MeteoDataProvider meteoProvider; // Assign in Inspector or auto-find

    private void Awake()
    {
        if (meteoProvider == null)
            meteoProvider = FindObjectOfType<MeteoDataProvider>();
    }

    private void Update()
    {
        float visibility, rain, airSpeed, showers;

        // Use API or manual values based on WeatherModeManager
        if (WeatherModeManager.Instance != null && !WeatherModeManager.Instance.useAPIMode)
        {
            visibility = WeatherModeManager.Instance.manualVisibility;
            rain = WeatherModeManager.Instance.manualRain;
            airSpeed = WeatherModeManager.Instance.manualAirSpeed;
            showers = WeatherModeManager.Instance.manualShowers;
        }
        else if (meteoProvider != null)
        {
            visibility = meteoProvider.Visibility;
            rain = meteoProvider.Rain;
            airSpeed = meteoProvider.AirSpeed;
            showers = meteoProvider.Showers;
        }
        else
        {
            // Fallback defaults
            visibility = 10000f;
            rain = 0f;
            airSpeed = 0f;
            showers = 0f;
        }

        UpdateParticles(visibility, rain, airSpeed,showers);
    }

    public void UpdateParticles(float visibility, float rain, float airSpeed,float showers)
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
                emission.rateOverTime = 500;
                var vol = rainParticleSystem.velocityOverLifetime;
                vol.radial = airSpeed >= 7 ? 0.1f : 0.05f;
                rainParticleSystem.Play();
            }
            else if(showers > 0.01f && showers <= 1f)
            {
                emission.rateOverTime = 2.5f;
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
    }
}