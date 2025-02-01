using System.Globalization;
using WeatherDashboard.Models;

namespace WeatherDashboard.Services.WeatherService
{
    public interface IWeatherService
    {
        Task StoreWeatherDataFromApiAsync(string apiUrl);
        Task StoreWeatherForecastFromXmlAsync(string xmlUrl);
        Task<IEnumerable<WeatherData>> GetAllWeatherAsync();
        Task<WeatherData> GetWeatherByLocationAsync(string location);
        Task<WeatherForecast> GetWeatherForecastByLocationAsync(string location);
        Task<WeatherData> GetNearestWeatherAsync(string[] coordinates);
    }
}
