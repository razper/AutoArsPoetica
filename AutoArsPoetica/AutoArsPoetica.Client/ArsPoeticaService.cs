using System;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ArsPoeticaService : IArsPoeticaService
{
    private readonly HttpClient _httpClient;
    public ArsPoeticaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<string> GenerateWeatherPoemAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<string>("/api/weather/generate");
        return response;
    }

    public async Task<string> GenerateCryptoPoemAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<string>("/api/crypto/generate");
        return response;
    }
}
