using System.ClientModel;
using AutoArsPoetica.Services;
using OpenAI.Chat;

internal class ArsPoeticaServiceBackEnd : IArsPoeticaService
{
    private readonly ChatClient _chatClient;
    private readonly IWeatherService _weatherService;

    public ArsPoeticaServiceBackEnd(ChatClient chatClient, IWeatherService weatherService)
    {
        _chatClient = chatClient;
        _weatherService = weatherService;
    }

    public async Task<string> GeneratePoemAsync()
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
}
