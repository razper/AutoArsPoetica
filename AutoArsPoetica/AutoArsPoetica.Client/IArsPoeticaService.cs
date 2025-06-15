public interface IArsPoeticaService
{
    Task<string> GenerateWeatherPoemAsync();
    Task<string> GenerateCryptoPoemAsync();
}
