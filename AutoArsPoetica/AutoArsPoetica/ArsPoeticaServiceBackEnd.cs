using System.ClientModel;
using AutoArsPoetica.Services;
using OpenAI.Chat;

internal class ArsPoeticaServiceBackEnd : IArsPoeticaService
{
    private readonly ChatClient _chatClient;
    private readonly IWeatherService _weatherService;
    private readonly ICryptoService _cryptoService;

    public ArsPoeticaServiceBackEnd(ChatClient chatClient, IWeatherService weatherService, ICryptoService cryptoService)
    {
        _chatClient = chatClient;
        _weatherService = weatherService;
        _cryptoService = cryptoService;
    }

    public async Task<string> GenerateWeatherPoemAsync()
    {
        try
        {
            var haifaWeather = await _weatherService.GetWeatherDataAsync();
            var prompt = WeatherPoemPromptGenerator.GeneratePrompt(haifaWeather);

            var poem = await _chatClient.CompleteChatAsync(prompt);
            return poem.Value.Content.First().Text;
        }
        catch (Exception ex)
        {
            throw new Exception("Error generating poem", ex);
        }
    }

    public async Task<string> GenerateCryptoPoemAsync()
    {
        try
        {
            var cryptoData = await _cryptoService.GetCryptoDataAsync("BTC-USD");
            var prompt = CryptoService.GeneratePrompt(cryptoData);
            var poem = await _chatClient.CompleteChatAsync(prompt);
            return poem.Value.Content.First().Text;
        }
        catch (Exception ex)
        {
            throw new Exception("Error generating crypto poem", ex);
        }
    }
}
