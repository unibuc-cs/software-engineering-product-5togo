namespace WeatherDashboard.Models
{
    public class APIResponse
    {
        public bool Success { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public List<Feature> Features { get; set; }
    }
}
