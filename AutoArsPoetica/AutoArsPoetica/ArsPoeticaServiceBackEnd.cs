using AutoArsPoetica;
using AutoArsPoetica.Services;
using Microsoft.EntityFrameworkCore;
using OpenAI.Chat;

internal class ArsPoeticaServiceBackEnd : IArsPoeticaService
{
    private readonly ChatClient _chatClient;
    private readonly IWeatherService _weatherService;
    private readonly ICryptoService _cryptoService;
    private readonly ArsPoeticaDbContext _dbContext;

    public ArsPoeticaServiceBackEnd(ChatClient chatClient, IWeatherService weatherService, ICryptoService cryptoService, IServiceProvider serviceProvider)
    {
        _chatClient = chatClient;
        _weatherService = weatherService;
        _cryptoService = cryptoService;
        _dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ArsPoeticaDbContext>();
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

    public async Task<List<AutoArsPoetica.Client.Models.Poem>> FetchPoemsAsync()
    {
        var poems = await _dbContext.Poems.OrderByDescending(p => p.Epoch).Take(23).ToListAsync();
        return poems.Select(p => new AutoArsPoetica.Client.Models.Poem
        {
            Id = p.Id,
            Content = p.Content,
            CreatedAt = p.CreatedAt,
            Epoch = p.Epoch
        }).ToList();
    }

    public async Task<WeatherDto> FetchWeatherAsync()
    {
        return await _dbContext.Weather.FirstOrDefaultAsync();
    }

    public async Task<CryptoDto> FetchCryptoAsync()
    {
        return await _dbContext.Crypto.FirstOrDefaultAsync();
    }
}
