using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherDashboard.Services.WeatherService;

namespace WeatherDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpPost("store-from-api")]
        public async Task<IActionResult> StoreWeatherDataFromApi()
        {
            var apiUrl = "https://www.meteoromania.ro/wp-json/meteoapi/v2/starea-vremii";

            try
            {
                await _weatherService.StoreWeatherDataFromApiAsync(apiUrl);
                return Ok("Weather data successfully stored in the database.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWeather()
        {
            var weatherData = await _weatherService.GetAllWeatherAsync();
            return Ok(weatherData);
        }

        [HttpGet("location/{locationName}")]
        public async Task<IActionResult> GetWeatherForLocation(string locationName)
        {
            var weatherData = await _weatherService.GetWeatherByLocationAsync(locationName);
            if (weatherData == null)
            {
                return NotFound(new { Message = $"No weather data found for {locationName}." });
            }

            return Ok(weatherData);
        }
    }
}
