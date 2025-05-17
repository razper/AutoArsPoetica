using AutoArsPoetica.Models;

namespace AutoArsPoetica.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<WeatherData> GetWeatherDataAsync()
    {
        // Note: You'll need to replace this with your actual weather API endpoint and key
        var apiKey = _configuration["WeatherApi:ApiKey"] ?? throw new InvalidOperationException("Weather API key not found in configuration");
        var baseUrl = _configuration["WeatherApi:BaseUrl"] ?? throw new InvalidOperationException("Weather API base URL not found in configuration");

        try
        {
            var response = await _httpClient.GetAsync($"{baseUrl}?lat=32.794044&lon=34.989571&appid={apiKey}");
            response.EnsureSuccessStatusCode();
            var weatherData = await response.Content.ReadFromJsonAsync<WeatherData>();
            return weatherData ?? throw new Exception("No weather data received");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching weather data: {ex.Message}");
        }
    }
}


public static class WeatherPoemPromptGenerator
{
    public static string GeneratePrompt(WeatherData weather)
    {
        return $@"
You are a poet generating a short free-verse poem (6–10 lines) based on current weather conditions. Use the following weather data to influence both the *rhythm* and *lyrical content* of the poem. The poem should reflect the emotional and structural characteristics associated with each weather parameter.

Weather data:
- Cloudiness: {weather.Clouds.All}%
- Temperature: {weather.Main.Temperature}°C
- Humidity: {weather.Main.Humidity}%
- Wind Speed: {weather.Wind.Speed} km/h
- Precipitation Type: {weather.Weather.FirstOrDefault()?.Description ?? "Unknown"}
- Visibility: {weather.Visibility} km
- Atmospheric Pressure: {weather.Main.Pressure} hPa

Instructions:
- Use **rhythmic variations** based on weather:
  - More cloudiness = slower, murkier rhythm.
  - High humidity = irregular rhythm, flowing or disoriented.
  - Fast wind = short, choppy lines.
  - Precipitation = rhythm should echo the fall (rain = soft, snow = slow, hail = sharp).
  - High pressure = tight, tense structure.
- Use **lyrical tone and imagery** based on weather:
  - Cloudiness = heavier, darker emotions.
  - Temperature = hotter = fiery words; colder = calm or still.
  - Humidity = blurry, vague expressions.
  - Wind = fragmented or interrupted thoughts.
  - Precipitation type = match imagery (rain = soft and nostalgic, snow = still and slow, hail = abrupt or jarring).
  - Low visibility = confusion, hidden meanings, foggy lines.
  - High pressure = anxious, compressed emotion.

Generate the poem in English. Do not mention the weather data explicitly—let it shape the poem subtly through mood and rhythm.";
    }
}