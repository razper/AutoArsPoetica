using AutoArsPoetica;
using AutoArsPoetica.Client.Models;

public interface IArsPoeticaService
{
    Task<string> GenerateWeatherPoemAsync();
    Task<string> GenerateCryptoPoemAsync();
    Task<List<Poem>> FetchPoemsAsync();
    Task<WeatherDto> FetchWeatherAsync();
    Task<CryptoDto> FetchCryptoAsync();
}
