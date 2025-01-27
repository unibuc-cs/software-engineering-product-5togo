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

        public async Task<WeatherData> GetByIdAsync(int id)
        {
            return await _context.WeatherRecords.FindAsync(id);
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
    }
}
