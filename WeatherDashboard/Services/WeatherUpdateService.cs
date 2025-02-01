using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherDashboard.Services.WeatherService;

public class WeatherUpdateService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TimeSpan _updateInterval = TimeSpan.FromHours(1);

    public WeatherUpdateService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var weatherService = scope.ServiceProvider.GetRequiredService<WeatherService>();

                    Console.WriteLine($"[{DateTime.Now}] Fetching weather data...");
                    await weatherService.StoreWeatherDataFromApiAsync("https://www.meteoromania.ro/wp-json/meteoapi/v2/starea-vremii");
                    await weatherService.StoreWeatherForecastFromXmlAsync("https://www.meteoromania.ro/anm/prognoza-orase-xml.php");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating weather data: {ex.Message}");
            }

            await Task.Delay(_updateInterval, stoppingToken);
        }
    }
}
