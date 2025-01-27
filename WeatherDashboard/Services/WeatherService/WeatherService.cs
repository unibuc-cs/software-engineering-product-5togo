using System.Text.Json;
using WeatherDashboard.Models;
using WeatherDashboard.Repositories.WeatherRepository;

namespace WeatherDashboard.Services.WeatherService
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(HttpClient httpClient, IWeatherRepository weatherRepository)
        {
            _httpClient = httpClient;
            _weatherRepository = weatherRepository;
        }

        public async Task StoreWeatherDataFromApiAsync(string apiUrl)
        {
            // Send request to Meteoromania API
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            // Deserialize JSON response in the APIResponse model
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var weatherResponse = JsonSerializer.Deserialize<APIResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Map response data into WeatherData model
            var weatherDataList = new List<WeatherData>();

            foreach (var feature in weatherResponse.Features)
            {
                var weatherData = new WeatherData
                {
                    LocationName = feature.Properties.Nume,
                    Temperature = float.TryParse(feature.Properties.Tempe, out var temp) ? temp : 0f,
                    Humidity = feature.Properties.Umezeala,
                    Phenomenon = feature.Properties.FenomenE,
                    Nebulozity = feature.Properties.Nebulozitate,
                    Pressure = feature.Properties.PresiuneText,
                    Wind = feature.Properties.Vant,
                    UpdatedAt = DateTime.Now
                };

                weatherDataList.Add(weatherData);
            }

            // Add data to the database
            foreach (var weatherData in weatherDataList)
            {
                await _weatherRepository.AddAsync(weatherData);
            }
        }

        public async Task<IEnumerable<WeatherData>> GetAllWeatherAsync()
        {
            return await _weatherRepository.GetAllAsync();
        }
    }
}
