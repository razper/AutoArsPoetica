using System.Text.Json.Serialization;

namespace AutoArsPoetica.Models;

public class WeatherData
{
    [JsonPropertyName("coord")]
    public Coordinates Coordinates { get; set; } = new();

    [JsonPropertyName("weather")]
    public List<WeatherCondition> Weather { get; set; } = new();

    [JsonPropertyName("base")]
    public string Base { get; set; } = string.Empty;

    [JsonPropertyName("main")]
    public MainWeather Main { get; set; } = new();

    [JsonPropertyName("visibility")]
    public int Visibility { get; set; }

    [JsonPropertyName("wind")]
    public Wind Wind { get; set; } = new();

    [JsonPropertyName("clouds")]
    public Clouds Clouds { get; set; } = new();

    [JsonPropertyName("dt")]
    public long DateTime { get; set; }

    [JsonPropertyName("sys")]
    public SystemInfo System { get; set; } = new();

    [JsonPropertyName("timezone")]
    public int Timezone { get; set; }

    [JsonPropertyName("id")]
    public int CityId { get; set; }

    [JsonPropertyName("name")]
    public string CityName { get; set; } = string.Empty;

    [JsonPropertyName("cod")]
    public int Code { get; set; }
}

public class Coordinates
{
    [JsonPropertyName("lon")]
    public double Longitude { get; set; }

    [JsonPropertyName("lat")]
    public double Latitude { get; set; }
}

public class MainWeather
{
    [JsonPropertyName("temp")]
    public double Temperature { get; set; }

    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; set; }

    [JsonPropertyName("temp_min")]
    public double TempMin { get; set; }

    [JsonPropertyName("temp_max")]
    public double TempMax { get; set; }

    [JsonPropertyName("pressure")]
    public int Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("sea_level")]
    public int SeaLevel { get; set; }

    [JsonPropertyName("grnd_level")]
    public int GroundLevel { get; set; }
}

public class Wind
{
    [JsonPropertyName("speed")]
    public double Speed { get; set; }

    [JsonPropertyName("deg")]
    public int Direction { get; set; }

    [JsonPropertyName("gust")]
    public double Gust { get; set; }
}

public class Clouds
{
    [JsonPropertyName("all")]
    public int All { get; set; }
}

public class SystemInfo
{
    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("sunrise")]
    public long Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public long Sunset { get; set; }
}

public class CurrentWeather
{
    [JsonPropertyName("dt")]
    public long DateTime { get; set; }

    [JsonPropertyName("sunrise")]
    public long Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public long Sunset { get; set; }

    [JsonPropertyName("temp")]
    public double Temperature { get; set; }

    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; set; }

    [JsonPropertyName("pressure")]
    public int Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("dew_point")]
    public double DewPoint { get; set; }

    [JsonPropertyName("uvi")]
    public double UVIndex { get; set; }

    [JsonPropertyName("clouds")]
    public int Cloudiness { get; set; }

    [JsonPropertyName("visibility")]
    public int Visibility { get; set; }

    [JsonPropertyName("wind_speed")]
    public double WindSpeed { get; set; }

    [JsonPropertyName("wind_deg")]
    public int WindDirection { get; set; }

    [JsonPropertyName("weather")]
    public List<WeatherCondition> Weather { get; set; } = new();
}

public class DailyWeather
{
    [JsonPropertyName("dt")]
    public long DateTime { get; set; }

    [JsonPropertyName("sunrise")]
    public long Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public long Sunset { get; set; }

    [JsonPropertyName("temp")]
    public Temperature Temp { get; set; } = new();

    [JsonPropertyName("feels_like")]
    public FeelsLike FeelsLike { get; set; } = new();

    [JsonPropertyName("pressure")]
    public int Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("wind_speed")]
    public double WindSpeed { get; set; }

    [JsonPropertyName("wind_deg")]
    public int WindDirection { get; set; }

    [JsonPropertyName("weather")]
    public List<WeatherCondition> Weather { get; set; } = new();

    [JsonPropertyName("clouds")]
    public int Cloudiness { get; set; }

    [JsonPropertyName("pop")]
    public double ProbabilityOfPrecipitation { get; set; }

    [JsonPropertyName("uvi")]
    public double UVIndex { get; set; }
}

public class WeatherCondition
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("main")]
    public string Main { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = string.Empty;
}

public class Temperature
{
    [JsonPropertyName("day")]
    public double Day { get; set; }

    [JsonPropertyName("min")]
    public double Min { get; set; }

    [JsonPropertyName("max")]
    public double Max { get; set; }

    [JsonPropertyName("night")]
    public double Night { get; set; }

    [JsonPropertyName("eve")]
    public double Evening { get; set; }

    [JsonPropertyName("morn")]
    public double Morning { get; set; }
}

public class FeelsLike
{
    [JsonPropertyName("day")]
    public double Day { get; set; }

    [JsonPropertyName("night")]
    public double Night { get; set; }

    [JsonPropertyName("eve")]
    public double Evening { get; set; }

    [JsonPropertyName("morn")]
    public double Morning { get; set; }
}