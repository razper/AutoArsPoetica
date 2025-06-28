public interface IArsPoeticaService
{
    Task<string> GenerateWeatherPoemAsync();
    Task<string> GenerateCryptoPoemAsync();
    Task<List<AutoArsPoetica.Client.Models.Poem>> FetchPoemsAsync();
}
