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
    public async Task<string> GeneratePoemAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<string>("/api/poem/generate");
        return response;
    }
}
