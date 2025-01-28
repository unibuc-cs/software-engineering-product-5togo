using System.Text.Json;

namespace AuthService.Services
{
    public class WeatherDashboardClient
    {
        private readonly HttpClient _httpClient;

        public WeatherDashboardClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
        }

        public async Task<object> GetWeatherDataForLocation(string locationName)
        {
            var response = await _httpClient.GetAsync($"location/{locationName}");
            //if (!response.IsSuccessStatusCode)
            //{
            //    throw new Exception($"Failed to fetch weather data for {locationName}. Status: {response.StatusCode}");
            //}

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<object>(content);
        }
    }
}
