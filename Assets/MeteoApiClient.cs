using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MeteoAPIClient : MonoBehaviour
{
    public event Action<string> OnApiResponse;

    [Header("API Settings")]
    public double latitude = 41.375;
    public double longitude = -8.75;
    public float refreshInterval = 3600f; // seconds

    private void Start()
    {
        StartCoroutine(RequestDataRoutine());
        InvokeRepeating(nameof(RequestData), refreshInterval, refreshInterval);
    }

    public void RequestData()
    {
        StartCoroutine(RequestDataRoutine());
    }

    private IEnumerator RequestDataRoutine()
    {
        string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=wind_speed_10m,wind_direction_10m,visibility,showers,rain";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                OnApiResponse?.Invoke(www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("API request failed: " + www.error);
            }
        }
    }
}