using System.Threading.Tasks;

namespace AutoArsPoetica.Services;

public interface IFlightService
{
    Task<FlightData> GetFlightDataAsync(string flightNumber);
}

public class FlightData
{
    public string FlightNumber { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string Status { get; set; } = string.Empty;
}