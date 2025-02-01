using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
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
                    Coordinates = new List<string>
                    {
                        feature.Geometry.Coordinates[0].ToString(),
                        feature.Geometry.Coordinates[1].ToString()
                    },
                    UpdatedAt = DateTime.Now
                };

                weatherDataList.Add(weatherData);
            }

            foreach (var weatherData in weatherDataList)
            {
                await _weatherRepository.AddAsync(weatherData);
            }
        }

        public async Task StoreWeatherForecastFromXmlAsync(string xmlUrl)
        {
            var response = await _httpClient.GetAsync(xmlUrl);
            response.EnsureSuccessStatusCode();

            var byteArray = await response.Content.ReadAsByteArrayAsync();

            var xmlString = Encoding.UTF8.GetString(byteArray);

            var xmlDoc = XDocument.Parse(xmlString);

            var weatherForecasts = new List<WeatherForecast>();

            foreach (var locationElement in xmlDoc.Descendants("localitate"))
            {
                string locationName = locationElement.Attribute("nume")?.Value ?? "Unknown";
                DateTime forecastDate = DateTime.Parse(locationElement.Element("DataPrognozei")?.Value ?? DateTime.Now.ToString());

                var weatherForecast = new WeatherForecast
                {
                    LocationName = locationName,
                    ForecastDate = forecastDate,
                    Forecasts = new List<Forecast>()
                };

                foreach (var forecastElement in locationElement.Descendants("prognoza"))
                {
                    var forecast = new Forecast
                    {
                        Date = DateTime.Parse(forecastElement.Attribute("data")?.Value ?? DateTime.Now.ToString()),
                        MinTemperature = float.TryParse(forecastElement.Element("temp_min")?.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var minTemp) ? minTemp : 0f,
                        MaxTemperature = float.TryParse(forecastElement.Element("temp_max")?.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var maxTemp) ? maxTemp : 0f,
                        Condition = forecastElement.Element("fenomen_descriere")?.Value ?? "Unknown",
                        Symbol = forecastElement.Element("fenomen_simbol")?.Value ?? "Unknown"
                    };

                    weatherForecast.Forecasts.Add(forecast);
                }

                weatherForecasts.Add(weatherForecast);
            }

            await _weatherRepository.ClearForecastAsync();
            foreach (var weatherForecast in weatherForecasts)
            {
                await _weatherRepository.AddWeatherForecastAsync(weatherForecast);
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
        public async Task<WeatherForecast> GetWeatherForecastByLocationAsync(string location)
        {
            return await _weatherRepository.GetForecastByLocationAsync(location);
        }

        public async Task<WeatherData> GetNearestWeatherAsync(string[] coordinates)
        {
            if (coordinates.Length < 2) return null;

            if (!double.TryParse(coordinates[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude) ||
                !double.TryParse(coordinates[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude))
            {
                return null;
            }

            return await _weatherRepository.GetNearestWeatherAsync(latitude, longitude);
        }
    }
}
