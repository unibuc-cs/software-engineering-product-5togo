using WeatherDashboard.Controllers;
using WeatherDashboard.Services.WeatherService;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using WeatherDashboard.Models;

namespace WeatherDashboard.Tests.Controllers
{
    public class WeatherControllerTests
    {
        private readonly IWeatherService _weatherServiceMock;
        private readonly WeatherController SUT;
        
        public WeatherControllerTests() 
        {
            _weatherServiceMock = A.Fake<IWeatherService>();
            SUT = new WeatherController(_weatherServiceMock);
        }

        [Fact]
        public async Task WeatherController_StoreWeatherDataFromApi_WhenServiceSucceeds()
        {
            // Arrange
            A.CallTo(() => _weatherServiceMock.StoreWeatherDataFromApiAsync(A<string>.Ignored)).Returns(Task.CompletedTask);

            // Act
            IActionResult result =await SUT.StoreWeatherDataFromApi();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().Be("Weather data successfully stored in the database.");

        }

        [Fact]
        public async Task WeatherController_StoreWeatherDataFromApi_WhenServiceThrowsException()
        {
            // Arrange
            A.CallTo(() => _weatherServiceMock.StoreWeatherDataFromApiAsync(A<string>.Ignored)).ThrowsAsync(new Exception("Service error"));

            // Act
            IActionResult result = await SUT.StoreWeatherDataFromApi();

            // Assert
            var statusCode = Assert.IsType<ObjectResult>(result);
            var errorMessage = statusCode.Value as string;
            statusCode.StatusCode.Should().Be(500);
            errorMessage.Should().Contain("An error occurred:");

        }

        [Fact]
        public async Task WeatherController_GetAllWeather_WhenServiceSucceeds()
        {
            // Arrange
            var weatherDataMock = A.Fake<IEnumerable<WeatherData>>();
            A.CallTo(() => _weatherServiceMock.GetAllWeatherAsync()).Returns(weatherDataMock);
            // Act
            var result = await SUT.GetAllWeather();
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().Be(weatherDataMock);
        }

        [Fact]
        public async Task WeatherController_GetWeatherForLocation_WhenServiceSucceeds()
        {
            // Arrange
            var weatherDataMock = A.Fake<WeatherData>();
            A.CallTo(() => _weatherServiceMock.GetWeatherByLocationAsync(A<string>.Ignored)).Returns(weatherDataMock);
            // Act
            var result = await SUT.GetAllWeather();
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }
    }
}
