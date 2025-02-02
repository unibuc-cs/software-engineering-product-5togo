using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WeatherDashboard.Data;
using WeatherDashboard.Models;
using WeatherDashboard.Repositories.WeatherRepository;

namespace WeatherDashboard.Tests.Repositories
{
    public class WeatherRepositoryTests
    {
        private readonly AppDbContext _mockContext;
        private readonly WeatherRepository SUT;

        public WeatherRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _mockContext = new AppDbContext(options);
            SUT = new WeatherRepository(_mockContext);
        }

        [Fact]
        public async Task WeatherRepository_GetAllAsync()
        {
            // Arrange
            var mockData = generateAMockDataWithLocation("Arad");
            _mockContext.WeatherRecords.Add(mockData);

            // Act
            var result = await SUT.GetAllAsync();

            // Assert
            result.Should().Contain(mockData);
        }

        //[Fact]
        //public async Task WeatherRepository_GetByLocationAsync()
        //{
        //    // Arrange
        //    var mockData = generateAMockDataWithLocation("Arad");
        //    _mockContext.WeatherRecords.Add(mockData);

        //    // Act
        //    var result = await SUT.GetByLocationAsync("Arad");

        //    // Assert
        //    result.Should().Be(mockData);
        //}

        [Fact]
        public async Task WeatherRepository_AddAsync()
        {
            // Arrange
            var mockData = generateAMockDataWithLocation("Arad");

            // Act
            await SUT.AddAsync(mockData);
            var result = await _mockContext.WeatherRecords.FirstOrDefaultAsync();

            // Assert
            result.Should().Be(mockData);
        }

        //[Fact]
        //public async Task WeatherRepository_UpdateAsync()
        //{
        //    // Arrange
        //    var mockData1 = generateAMockDataWithLocation("Arad");
        //    var mockData2 = generateAMockDataWithLocation("Bucuresti");
        //    _mockContext.WeatherRecords.Add(mockData1);

        //    // Act
        //    await SUT.UpdateAsync(mockData2);
        //    var result = await _mockContext.WeatherRecords.FindAsync(1);

        //    // Assert
        //    result.LocationName.Should().Be("Bucuresti");
        //}

        [Fact]
        public async Task WeatherRepository_DeleteAsync_WhenIdIsFound()
        {
            // Arrange
            var mockData = generateAMockDataWithLocation("Bucuresti");
            _mockContext.WeatherRecords.Add(mockData);

            // Act
            await SUT.DeleteAsync(1);
            var result = await _mockContext.WeatherRecords.FirstOrDefaultAsync();

            // Assert
            result.Should().BeNull();
            }

        [Fact]
        public async Task WeatherRepository_DeleteAsync_WhenIdIsNotFound()
        {
            // Arrange
            var mockData = generateAMockDataWithLocation("Bucuresti");
            _mockContext.WeatherRecords.Add(mockData);

            // Act
            await SUT.DeleteAsync(2);
            var result = await _mockContext.WeatherRecords.FindAsync(1);

            // Assert
            result.Should().NotBeNull();
        }

        private static WeatherData generateAMockDataWithLocation(String locatie)
        {
            return new WeatherData()
            {
                Id = 1,
                LocationName = locatie
            };
        }

        public async void Dispose()
        {
            await SUT.ClearAsync();
        }

    }
}
