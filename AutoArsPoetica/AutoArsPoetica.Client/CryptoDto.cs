namespace AutoArsPoetica;

public class CryptoDto
{
    public string Id { get; set; } = "1";
    public string Price { get; set; }
    public string PriceChange24h { get; set; }
    public string Volume24h { get; set; }
    public double Ask { get; set; }
    public double Bid { get; set; }
    public TradingStatus Trading { get; set; }
    public string Status { get; set; }
    public string ProductId { get; set; }
}

public enum TradingStatus
{
    Enabled,
    Disabled
}