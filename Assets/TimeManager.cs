using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Texture2D skyboxNight;
    [SerializeField] private Texture2D skyboxSunrise;
    [SerializeField] private Texture2D skyboxDay;
    [SerializeField] private Texture2D skyboxSunset;
    [Header("Manual Time Control")]
    public float manualHour = 12f;

    [SerializeField] private Gradient graddientNightToSunrise;
    [SerializeField] private Gradient graddientSunriseToDay;
    [SerializeField] private Gradient graddientDayToSunset;
    [SerializeField] private Gradient graddientSunsetToNight;
    [SerializeField] private Color nigthFog;
    [SerializeField] private Color sunRiseFog;
    [SerializeField] private Color DayFog;
    [SerializeField] private Color SunSetFog;
    public LightingSettingsSwitcher switcher;

    [SerializeField] private Light globalLight;
    private WeatherModeManager modeManager;

    private int minutes;

    public int Minutes
    { get { return minutes; } set { minutes = value; OnMinutesChange(value); } }

    private int hours = 5;

    public int Hours
    { get { return hours; } set { hours = value; OnHoursChange(value); } }

    private int days;

    public int Days
    { get { return days; } set { days = value; } }

    private float tempSecond;

    public void Start()
    {
        Minutes = DateTime.Now.Minute;
        Hours = DateTime.Now.Hour;
        Days = DateTime.Now.Day;
        Debug.Log($"Current Time: {Days} Days, {Hours} Hours, {Minutes} Minutes");
        modeManager = WeatherModeManager.Instance;

        OnHoursChange(Hours);
        OnStartSkybox(Hours);

    }
    public void Update()
    {
        if (modeManager != null && !modeManager.useAPIMode)
        {
            Hours = Mathf.FloorToInt(manualHour);
        }
        else
        {
            Hours = DateTime.Now.Hour;
            Minutes = DateTime.Now.Minute;
            Days = DateTime.Now.Day;
        }
        tempSecond += Time.deltaTime;
    }
    public void SetManualHour(float value)
    {
        manualHour = value;
        Debug.Log($"Manual Hour set to {manualHour}");
    }

    private void OnMinutesChange(int value)
    {
        float totalMinutes = Hours * 60f + Minutes;
        float sunAngle = (totalMinutes / 1440f) * 360f;

        globalLight.transform.rotation = Quaternion.Euler(new Vector3(sunAngle, 0f, 0f));
        if (value >= 60)
        {
            Hours++;
            minutes = 0;
        }
        if (Hours >= 24)
        {
            Hours = 0;
            Days++;
        }
    }

    private void OnStartSkybox(int hours)
    {
        if(hours >= 6 && hours <= 12)
        {
            //Sunrise
            UnityEngine.RenderSettings.fogColor = sunRiseFog;
            StartCoroutine(LerpSkybox(skyboxNight, skyboxSunrise, 10f));
            StartCoroutine(LerpLight(graddientNightToSunrise, 10f));
            switcher.ApplyLightingSettings(2);

            Debug.Log("Sunrise Skybox and Lighting Applied");
        }
        else if (hours >= 12 && hours <= 20)
        {
            //Day

            UnityEngine.RenderSettings.fogColor = DayFog;
            StartCoroutine(LerpSkybox(skyboxSunrise, skyboxDay, 10f));
            StartCoroutine(LerpLight(graddientSunriseToDay, 10f));
            switcher.ApplyLightingSettings(3);
            Debug.Log("Day Skybox and Lighting Applied");
        }
        else if (hours >= 20 && hours <= 22)
        {
            //Sunset
            UnityEngine.RenderSettings.fogColor = SunSetFog ;
            StartCoroutine(LerpSkybox(skyboxDay, skyboxSunset, 10f));
            StartCoroutine(LerpLight(graddientDayToSunset, 10f));
            switcher.ApplyLightingSettings(4);
            Debug.Log("Sunset Skybox and Lighting Applied");
        }
        else if(hours >= 22 || hours < 6)
        {
            //Night
            StartCoroutine(LerpSkybox(skyboxSunset, skyboxNight, 10f));
            StartCoroutine(LerpLight(graddientSunsetToNight, 10f));
            switcher.ApplyLightingSettings(1);
            UnityEngine.RenderSettings.fogColor = nigthFog;
            Debug.Log("Night Skybox and Lighting Applied");
        }
    }

    private void OnHoursChange(int value)
    {
        if (value == 6)
        {
            StartCoroutine(LerpSkybox(skyboxNight, skyboxSunrise, 10f));
            StartCoroutine(LerpLight(graddientNightToSunrise, 10f));
            switcher.ApplyLightingSettings(2);
        }
        else if (value == 8)
        {
            StartCoroutine(LerpSkybox(skyboxSunrise, skyboxDay, 10f));
            StartCoroutine(LerpLight(graddientSunriseToDay, 10f));
            switcher.ApplyLightingSettings(3);
        }
        else if (value == 18)
        {
            StartCoroutine(LerpSkybox(skyboxDay, skyboxSunset, 10f));
            StartCoroutine(LerpLight(graddientDayToSunset, 10f));
            switcher.ApplyLightingSettings(4);
        }
        else if (value == 22)
        {
            StartCoroutine(LerpSkybox(skyboxSunset, skyboxNight, 10f));
            StartCoroutine(LerpLight(graddientSunsetToNight, 10f));
            switcher.ApplyLightingSettings(1);
        }
    }

    private IEnumerator LerpSkybox(Texture2D a, Texture2D b, float time)
    {
        UnityEngine.RenderSettings.skybox.SetTexture("_Texture1", a);
        UnityEngine.RenderSettings.skybox.SetTexture("_Texture2", b);
        UnityEngine.RenderSettings.skybox.SetFloat("_Blend", 0);
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            UnityEngine.RenderSettings.skybox.SetFloat("_Blend", i / time);
            yield return null;
        }
        UnityEngine.RenderSettings.skybox.SetTexture("_Texture1", b);
    }

    private IEnumerator LerpLight(Gradient lightGradient, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            globalLight.color = lightGradient.Evaluate(i / time);
            UnityEngine.RenderSettings.fogColor = globalLight.color;
            yield return null;
        }
    }
}