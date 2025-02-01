using WeatherDashboard.Models;

namespace WeatherDashboard.Repositories.WeatherRepository
{
    public interface IWeatherRepository
    {
        Task<IEnumerable<WeatherData>> GetAllAsync();
        Task<WeatherData> GetByLocationAsync(string location);
        Task AddAsync(WeatherData weatherData);
        Task UpdateAsync(WeatherData weatherData);
        Task DeleteAsync(int id);
        Task ClearAsync();
        Task ClearForecastAsync();
        Task AddWeatherForecastAsync(WeatherForecast forecast);
        Task<WeatherForecast> GetForecastByLocationAsync(string location);
        Task<WeatherData> GetNearestWeatherAsync(double latitude, double longitude);
    }
}
