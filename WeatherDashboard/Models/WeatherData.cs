namespace WeatherDashboard.Models
{
    public class WeatherData
    {
        public int Id { get; set; }
        public string? LocationName { get; set; }
        public float Temperature { get; set; }
        public int Humidity { get; set; }
        public string? Phenomenon { get; set; }
        public string? Nebulozity { get; set; }
        public string? Pressure { get; set; }
        public string? Wind { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
