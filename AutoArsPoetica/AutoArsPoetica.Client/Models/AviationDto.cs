namespace AutoArsPoetica.Client.Models
{
    public class AviationDto
    {
        public int Aircrafts { get; set; }
        public int AverageSpeed { get; set; } // km/h
        public int Landings { get; set; }
        public int Takeoffs { get; set; }
        public string AircraftType { get; set; } = string.Empty;
        public int FlightDuration { get; set; } // minutes
        public string TimeOfDay { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty;
    }
}