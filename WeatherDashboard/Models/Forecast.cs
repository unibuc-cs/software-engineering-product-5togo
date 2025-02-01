namespace WeatherDashboard.Models
{
    public class Forecast
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float MinTemperature { get; set; }
        public float MaxTemperature { get; set; }
        public string Condition { get; set; }
        public string Symbol { get; set; }
        public int WeatherForecastId { get; set; }
        public WeatherForecast WeatherForecast { get; set; }
    }
}
