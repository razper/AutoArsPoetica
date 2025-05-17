using System.Threading.Tasks;
using AutoArsPoetica.Models;

namespace AutoArsPoetica.Services;

public interface IWeatherService
{
    Task<WeatherData> GetWeatherDataAsync();
}