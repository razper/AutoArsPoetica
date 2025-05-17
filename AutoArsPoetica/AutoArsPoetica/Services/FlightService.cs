using System.Net.Http.Json;

namespace AutoArsPoetica.Services;

public class FlightService : IFlightService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public FlightService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<FlightData> GetFlightDataAsync(string flightNumber)
    {
        // Note: You'll need to replace this with your actual flight data API endpoint and key
        var apiKey = _configuration["FlightApi:ApiKey"] ?? throw new InvalidOperationException("Flight API key not found in configuration");
        var baseUrl = _configuration["FlightApi:BaseUrl"] ?? throw new InvalidOperationException("Flight API base URL not found in configuration");

        _httpClient.DefaultRequestHeaders.Add("X-API-Key", apiKey);

        try
        {
            var response = await _httpClient.GetFromJsonAsync<FlightData>($"{baseUrl}/flights/{flightNumber}");
            return response ?? throw new Exception("No flight data received");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching flight data: {ex.Message}");
        }
    }
}