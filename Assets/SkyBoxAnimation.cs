using System;
using UnityEngine;

public class SkyboxAnimator : MonoBehaviour
{
    public Material skyboxMaterial;

    void Update()
    {
        float t = (DateTime.Now.Hour - 6f) / 12f;
        float elevation = Mathf.Lerp(-10f, 190f, t);
        float azimuth = 0f;

        float radElevation = Mathf.Deg2Rad * elevation;
        float radAzimuth = Mathf.Deg2Rad * azimuth;

        Vector3 sunDir = new Vector3(
            Mathf.Cos(radElevation) * Mathf.Cos(radAzimuth),
            Mathf.Sin(radElevation),
            Mathf.Cos(radElevation) * Mathf.Sin(radAzimuth)
        );
        sunDir.Normalize();

        skyboxMaterial.SetVector("_SunDirection", sunDir);
    }
}
