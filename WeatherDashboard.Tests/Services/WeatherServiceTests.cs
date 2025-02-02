using FakeItEasy;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using WeatherDashboard.Models;
using WeatherDashboard.Repositories.WeatherRepository;
using WeatherDashboard.Services.WeatherService;

namespace WeatherDashboard.Tests.Services
{
    public class WeatherServiceTests
    {
        private readonly HttpClient _httpClientMock;
        private readonly HttpMessageHandler _httpMessageHandlerMock;
        private readonly IWeatherRepository _weatherRepositoryMock;
        private readonly WeatherService SUT;

        public WeatherServiceTests() 
        {
            _httpMessageHandlerMock = A.Fake<HttpMessageHandler>(options => options.CallsBaseMethods());
            _httpClientMock = new HttpClient(_httpMessageHandlerMock)
            {
                BaseAddress = new Uri("https://fakeapi.com")
            };
            _weatherRepositoryMock = A.Fake<IWeatherRepository>();
            SUT = new WeatherService( _httpClientMock, _weatherRepositoryMock );
        }

        [Fact]
        public async Task WeatherService_StoreWeatherDataFromApiAsync_WhenAPIRetusrnValidData()
        {
            // Arrange
            var mockedApiResponse = new APIResponse
            {
                Features = new List<Feature>
                { new Feature
                    {
                        Properties = new WeatherProperties
                        {
                            Nume = "Bucuresti",
                            Tempe = "7",
                            Umezeala = 70,
                            Fenomen_e = "Fara",
                            Nebulozitate = "10",
                            PresiuneText = "1015 hPa",
                            Vant = "Moderat",
                            Zapada = "Nu",
                            Icon = "test",
                            TempApa = "2"
                        }
                    } 
                }
            };
            var mockedJsonResponse = JsonSerializer.Serialize(mockedApiResponse);
            var mockedHttpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(mockedJsonResponse, Encoding.UTF8, "application/json")
            };
            A.CallTo(_httpMessageHandlerMock)
                .WithReturnType<Task<HttpResponseMessage>>()
                .Returns(mockedHttpResponse);

            // Act
            await SUT.StoreWeatherDataFromApiAsync("https://www.meteoromania.ro/wp-json/meteoapi/v2/starea-vremii");

            // Assert
            A.CallTo(() => _weatherRepositoryMock.ClearAsync()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _weatherRepositoryMock.AddAsync(A<WeatherData>._)).MustHaveHappened();
        }
    }
}
