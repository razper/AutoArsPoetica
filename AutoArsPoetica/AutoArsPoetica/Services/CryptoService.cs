
namespace AutoArsPoetica.Services;


public class CryptoService : ICryptoService
{
    private readonly HttpClient _httpClient;

    public CryptoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CryptoData> GetCryptoDataAsync(string productId)
    {
        var response = await _httpClient.GetFromJsonAsync<CryptoData>($"https://api.coinbase.com/api/v3/brokerage/market/products/{productId}");
        return response;
    }

    public static string GeneratePrompt(CryptoData crypto)
    {
        return $@"
You are a poet generating a short free-verse poem. The poem must be 5 to 10 lines long, split into 1 or 2 stanzas. Each line must contain no more than 4 words.

The poem is shaped by real-time cryptocurrency data retrieved from a structured object called CryptoData. Use the following fields to influence both:

1. *Lyrical content* (emotional tone, imagery, vocabulary)  
2. *Visual structure* (line length, rhythm, punctuation)

Live fields (from CryptoData):
- Product ID: {crypto.ProductId}
- Current Price: {crypto.Price}
- 24h Price Change: {crypto.PricePercentageChange24h}
- 24h Volume: {crypto.Volume24h}
- Ask Price: {crypto.Ask}
- Bid Price: {crypto.Bid}
- Trading Enabled: {!crypto.TradingDisabled}
- Status: {crypto.Status}

*Guidelines:*

→ *Visual Style:*
- PricePercentageChange24h:  
  - Negative = broken rhythm, hesitation.  
  - Positive = fluid lines, pacing.

- Ask - Bid spread:  
  - Wide = open spacing, long pauses.  
  - Narrow = dense formatting, fast rhythm.

- TradingDisabled:  
  - True = fragmented lines, dashes, stillness.  
  - False = confident, continuous form.

- Status:  
  - “online” = verbs, energy, progression.  
  - Otherwise = calm, frozen, soft tone.

→ *Lyrical Content & Emotion:*
- Price:  
  - High = cosmic metaphors, transcendence.  
  - Low = humble tone, concrete images.

- PricePercentageChange24h:  
  - Negative = fragility, fear, nostalgia.  
  - Positive = hope, expansion, craving.

- Volume24h:  
  - High = noise, mass movement.  
  - Low = isolation, quietness.

- ProductId:  
  - If BTC-USD = existential, timeless, epic.  
  - Other = modern, digital, reactive.

*Important:*  
- Do *not* mention any numeric values or technical terms.  
- Do *not* use the word `whispers` directly you can synonymize it.  
- The reader should recognize it as a poem, even without knowing the context.

Generate the poem in *English only*, strictly following all constraints.";
    }
}

