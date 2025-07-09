namespace AutoArsPoetica;

public class WeatherDto
{
    public string Id { get; set; } = "1";
    public int Cloudiness { get; set; }
    public double Temperature { get; set; }
    public int Humidity { get; set; }
    public double WindSpeed { get; set; }
    public string PrecipitationType { get; set; }
    public string UVIndex { get; set; }
    public int Visibility { get; set; }
    public int Pressure { get; set; }
}