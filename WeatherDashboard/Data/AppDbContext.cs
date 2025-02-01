using Microsoft.EntityFrameworkCore;
using WeatherDashboard.Models;

namespace WeatherDashboard.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<WeatherData> WeatherRecords { get; set; }
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<Forecast> Forecasts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>()
                .HasMany(w => w.Forecasts)
                .WithOne(f => f.WeatherForecast)
                .HasForeignKey(f => f.WeatherForecastId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
