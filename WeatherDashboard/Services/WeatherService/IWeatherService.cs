using System.Globalization;
using WeatherDashboard.Models;

namespace WeatherDashboard.Services.WeatherService
{
    public interface IWeatherService
    {
        Task StoreWeatherDataFromApiAsync(string apiUrl);
        Task<IEnumerable<WeatherData>> GetAllWeatherAsync();
        Task<WeatherData> GetWeatherByLocationAsync(string location);
    }
}
