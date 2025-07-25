using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoArsPoetica;
using AutoArsPoetica.Client.Models;

public class ArsPoeticaService : IArsPoeticaService
{
    private readonly HttpClient _httpClient;
    public ArsPoeticaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<string> GenerateWeatherPoemAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<Poem>("/api/weather/generate");
        return response.Content;
    }

    public async Task<string> GenerateCryptoPoemAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<Poem>("/api/crypto/generate");
        return response.Content;
    }

    public async Task<List<Poem>> FetchPoemsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<Poem>>("/api/poems");
        return response;
    }

    public async Task<WeatherDto> FetchWeatherAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<WeatherDto>("/api/weather");
        return response;
    }

    public async Task<CryptoDto> FetchCryptoAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<CryptoDto>("/api/crypto");
        return response;
    }

}
