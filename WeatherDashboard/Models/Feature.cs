namespace WeatherDashboard.Models
{
    public class Feature
    {
        public string Type { get; set; }
        public Geometry Geometry { get; set; }
        public WeatherProperties Properties { get; set; }
    }
}
