using Microsoft.EntityFrameworkCore;
using WeatherDashboard.Models;

namespace WeatherDashboard.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<WeatherData> WeatherRecords { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
