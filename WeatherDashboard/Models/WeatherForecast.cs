namespace WeatherDashboard.Models
{
    public class WeatherForecast
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public DateTime ForecastDate { get; set; }
        public List<Forecast> Forecasts { get; set; } = new List<Forecast>();
    }
}
