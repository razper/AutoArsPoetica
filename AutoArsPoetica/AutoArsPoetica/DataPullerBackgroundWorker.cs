using AutoArsPoetica.Services;

namespace AutoArsPoetica;


public class DataPullerBackgroundWorker : BackgroundService
{
    private readonly ILogger<DataPullerBackgroundWorker> _logger;
    private readonly IWeatherService _weatherService;
    private readonly ICryptoService _cryptoService;
    private readonly IServiceProvider _serviceProvider;

    public DataPullerBackgroundWorker(ILogger<DataPullerBackgroundWorker> logger, IWeatherService weatherService, ICryptoService cryptoService, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _weatherService = weatherService;
        _cryptoService = cryptoService;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        WeatherDto weatherDto = null;
        DateTime lastWeatherUpdate = DateTime.MinValue;
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("DataPullerBackgroundWorker is running.");
            // Pull weather data once per minute
            if ((DateTime.UtcNow - lastWeatherUpdate).TotalSeconds >= 7200 || weatherDto == null)
            {
                var weather = await _weatherService.GetWeatherDataAsync();
                weatherDto = new WeatherDto
                {
                    Cloudiness = weather.Clouds.All,
                    Temperature = weather.Main.Temperature - 273.15,
                    Humidity = weather.Main.Humidity,
                    WindSpeed = weather.Wind.Speed,
                    PrecipitationType = weather.Weather[0].Description,
                    UVIndex = GetUVIndexByTimeOfDay(),
                    Visibility = weather.Visibility,
                    Pressure = weather.Main.Pressure,
                };
                lastWeatherUpdate = DateTime.UtcNow;
                _logger.LogInformation("Weather: {Weather}", weather);
            }
            var crypto = await _cryptoService.GetCryptoDataAsync("BTC-USD");
            _logger.LogInformation("Crypto: {Crypto}", crypto);
            var cryptoDto = new CryptoDto
            {
                Price = crypto.Price,
                PriceChange24h = crypto.PricePercentageChange24h,
                Volume24h = crypto.Volume24h,
                Ask = crypto.Ask,
                Bid = crypto.Bid,
                Trading = crypto.TradingDisabled ? TradingStatus.Disabled : TradingStatus.Enabled,
                Status = crypto.Status,
                ProductId = crypto.ProductId
            };
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ArsPoeticaDbContext>();
                if (weatherDto != null)
                {
                    // Assuming WeatherDto has a primary key property called Id
                    var existingWeather = dbContext.Weather.FirstOrDefault(w => w.Id == weatherDto.Id);
                    if (existingWeather != null)
                    {
                        // Update properties
                        existingWeather.Cloudiness = weatherDto.Cloudiness;
                        existingWeather.Temperature = weatherDto.Temperature;
                        existingWeather.Humidity = weatherDto.Humidity;
                        existingWeather.WindSpeed = weatherDto.WindSpeed;
                        existingWeather.PrecipitationType = weatherDto.PrecipitationType;
                        existingWeather.UVIndex = weatherDto.UVIndex;
                        existingWeather.Visibility = weatherDto.Visibility;
                        existingWeather.Pressure = weatherDto.Pressure;
                    }
                    else
                    {
                        dbContext.Weather.Add(weatherDto);
                    }
                }
                if (cryptoDto != null)
                {
                    var existingCrypto = dbContext.Crypto.FirstOrDefault(c => c.Id == cryptoDto.Id);
                    if (existingCrypto != null)
                    {
                        // Update properties
                        existingCrypto.Price = cryptoDto.Price;
                        existingCrypto.PriceChange24h = cryptoDto.PriceChange24h;
                        existingCrypto.Volume24h = cryptoDto.Volume24h;
                        existingCrypto.Ask = cryptoDto.Ask;
                        existingCrypto.Bid = cryptoDto.Bid;
                        existingCrypto.Trading = cryptoDto.Trading;
                        existingCrypto.Status = cryptoDto.Status;
                        existingCrypto.ProductId = cryptoDto.ProductId;
                    }
                    else
                    {
                        dbContext.Crypto.Add(cryptoDto);
                    }
                }
                await dbContext.SaveChangesAsync();
            }
            await Task.Delay(1000, stoppingToken);
        }
    }

    private string GetUVIndexByTimeOfDay()
    {
        var hour = DateTime.Now.Hour;
        var random = new Random();

        // UV index is typically highest during midday (10 AM - 4 PM)
        if (hour >= 10 && hour <= 16)
        {
            // High UV index during peak sun hours (6-10)
            return random.Next(6, 11).ToString();
        }
        else if (hour >= 6 && hour <= 9 || hour >= 17 && hour <= 19)
        {
            // Medium UV index during morning/evening (3-6)
            return random.Next(3, 7).ToString();
        }
        else
        {
            // Low UV index during early morning/late evening (0-3)
            return random.Next(0, 4).ToString();
        }
    }
}