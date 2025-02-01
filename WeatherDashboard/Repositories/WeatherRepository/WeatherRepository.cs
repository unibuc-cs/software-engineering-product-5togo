using Microsoft.EntityFrameworkCore;
using WeatherDashboard.Data;
using WeatherDashboard.Models;

namespace WeatherDashboard.Repositories.WeatherRepository
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly AppDbContext _context;

        public WeatherRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WeatherData>> GetAllAsync()
        {
            return await _context.WeatherRecords.ToListAsync();
        }

        public async Task<WeatherData> GetByLocationAsync(string location)
        {
            return await _context.WeatherRecords
                .Where(w => w.LocationName.ToLower() == location.ToLower())
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(WeatherData weatherData)
        {
            await _context.WeatherRecords.AddAsync(weatherData);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WeatherData weatherData)
        {
            _context.WeatherRecords.Update(weatherData);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var weatherData = await _context.WeatherRecords.FindAsync(id);
            if (weatherData != null)
            {
                _context.WeatherRecords.Remove(weatherData);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearAsync()
        {
            var allWeatherData = await _context.WeatherRecords.ToListAsync();
            _context.WeatherRecords.RemoveRange(allWeatherData);
            await _context.SaveChangesAsync();
        }

        public async Task ClearForecastAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM WeatherForecasts");
        }

        public async Task AddWeatherForecastAsync(WeatherForecast forecast)
        {
            await _context.WeatherForecasts.AddAsync(forecast);
            await _context.SaveChangesAsync();
        }

        public async Task<WeatherForecast> GetForecastByLocationAsync(string location)
        {
            return await _context.WeatherForecasts
                .Include(w => w.Forecasts)
                .FirstOrDefaultAsync(w => w.LocationName == location);
        }

        public async Task<WeatherData> GetNearestWeatherAsync(double latitude, double longitude)
        {
            var weatherDataList = await _context.WeatherRecords
                .ToListAsync();

            return weatherDataList
                .OrderBy(w =>
                {
                    var (lat, lon) = ConvertToLatLon(w.Coordinates[0], w.Coordinates[1]);
                    return HaversineDistance(latitude, longitude, lat, lon);
                })
                .FirstOrDefault();
        }

        private (double lat, double lon) ConvertToLatLon(string xStr, string yStr)
        {
            const double R = 6378137.0;

            if (!double.TryParse(xStr, out double x) || !double.TryParse(yStr, out double y))
            {
                throw new ArgumentException("Invalid coordinate format.");
            }

            double lon = (x / R) * 180.0 / Math.PI;
            double lat = (Math.Atan(Math.Sinh(y / R)) * 180.0) / Math.PI;

            return (lat, lon);
        }

        private double HaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371;

            double dLat = (lat2 - lat1) * Math.PI / 180;
            double dLon = (lon2 - lon1) * Math.PI / 180;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }
    }
}
