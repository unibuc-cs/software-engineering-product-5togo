using System.Globalization;
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
            await _weatherRepository.ClearAsync();

            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var weatherResponse = JsonSerializer.Deserialize<APIResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var weatherDataList = new List<WeatherData>();

            foreach (var feature in weatherResponse.Features)
            {
                var weatherData = new WeatherData
                {
                    LocationName = feature.Properties.Nume,
                    Temperature = float.TryParse(feature.Properties.Tempe,
                            NumberStyles.Float,
                            CultureInfo.InvariantCulture,
                            out var temp) ? temp : 0f,

                    Humidity = feature.Properties.Umezeala,
                    Phenomenon = feature.Properties.Fenomen_e,
                    Nebulozity = feature.Properties.Nebulozitate,
                    Pressure = feature.Properties.PresiuneText,
                    Wind = feature.Properties.Vant,
                    Snow = feature.Properties.Zapada,
                    Icon = feature.Properties.Icon,
                    WaterTemp = feature.Properties.TempApa,
                    UpdatedAt = DateTime.Now
                };

                weatherDataList.Add(weatherData);
            }

            foreach (var weatherData in weatherDataList)
            {
                await _weatherRepository.AddAsync(weatherData);
            }
        }


        public async Task<IEnumerable<WeatherData>> GetAllWeatherAsync()
        {
            return await _weatherRepository.GetAllAsync();
        }

        public async Task<WeatherData> GetWeatherByLocationAsync(string location)
        {
            return await _weatherRepository.GetByLocationAsync(location);
        }
    }
}
