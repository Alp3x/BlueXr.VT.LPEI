using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

// Main MonoBehaviour class
public class WaveDisplacementController : MonoBehaviour
{
    public Material targetMaterial;
    private string apiUrl = "https://marine-api.open-meteo.com/v1/marine?latitude=41.375&longitude=-8.75&hourly=wave_height&timezone=Europe%2FLondon";
    public TextMeshPro waveHeightText;

    void Start()
    {
        StartCoroutine(FetchWaveHeight());
    }

    IEnumerator FetchWaveHeight()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
            }
            else
            {
                string json = request.downloadHandler.text;
                MarineData data = JsonUtility.FromJson<MarineData>(json);

                if (data != null && data.hourly != null && data.hourly.time != null)
                {
                    DateTime currentTimeUtc = DateTime.UtcNow;
                    TimeSpan londonOffset = TimeSpan.FromHours(1); // Adjust as needed for DST
                    DateTime londonTime = currentTimeUtc + londonOffset;

                    string currentHour = londonTime.ToString("yyyy-MM-dd'T'HH':00'", CultureInfo.InvariantCulture);

                    int index = data.hourly.time.IndexOf(currentHour);
                    if (index != -1 && index < data.hourly.wave_height.Count)
                    {
                        float waveHeight = data.hourly.wave_height[index];

                        if (targetMaterial != null)
                        {
                            targetMaterial.SetFloat("_Displacement", waveHeight);
                        }
                        else
                        {
                        }
                        if (waveHeightText != null)
                        {
                            waveHeightText.text = $"Wave Height: {waveHeight:0.00} m";
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }
        }
    }

    [Serializable]
    public class HourlyData
    {
        public List<string> time;
        public List<float> wave_height;
    }

    [Serializable]
    public class MarineData
    {
        public HourlyData hourly;
    }
}
