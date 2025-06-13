using System;
using System.Collections.Generic;

[Serializable]
public class MeteoDataModel
{
    [Serializable]
    public class Hourly
    {
        public List<string> time;
        public List<float> wind_speed_10m;
        public List<int> wind_direction_10m;
        public List<float> visibility;
        public List<float> showers;
        public List<float> rain;
        public List<float> temperature_2m;
        public List<float> snowfall;
    }

    [Serializable]
    public class HourlyUnits
    {
        public string time;
        public string wind_speed_10m;
        public string wind_direction_10m;
    }

    [Serializable]
    public class Root
    {
        public float latitude;
        public float longitude;
        public float generationtime_ms;
        public int utc_offset_seconds;
        public string timezone;
        public string timezone_abbreviation;
        public float elevation;
        public HourlyUnits hourly_units;
        public Hourly hourly;
    }

    public Root root;
}