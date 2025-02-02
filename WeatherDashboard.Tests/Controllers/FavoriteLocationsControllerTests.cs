using System.Security.Claims;
using AuthService.Controllers;
using AuthService.Data;
using AuthService.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static AuthService.Controllers.FavoriteLocationsController;

namespace WeatherDashboard.Tests.Controllers
{
    public class FavoriteLocationsControllerTests
    {
        private readonly AuthDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly WeatherDashboardClient _weatherDashboardClient;
        private readonly FavoriteLocationsController SUT;

        public FavoriteLocationsControllerTests()
        {
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new AuthDbContext(options);
            _userManager = A.Fake<UserManager<IdentityUser>>();
            _weatherDashboardClient = A.Fake<WeatherDashboardClient>();

            SUT = new FavoriteLocationsController(_context, _userManager, _weatherDashboardClient);
        }

        [Fact]
        public async void FavoriteLocationsController_AddFavoriteLocation_WhenLocationIsEmpty()
        {
            // Arrange

            // Act
            var result = await SUT.AddFavoriteLocation(new FavoriteLocationRequest());

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            var resultMessage = badResult.Value as string;
            resultMessage.Should().Be("Location name is required.");
        }

        [Fact]
        public async void FavoriteLocationsController_AddFavoriteLocation_WhenUserIsNull() 
        {
            // Arrange
            A.CallTo(() => _userManager.GetUserAsync(A<ClaimsPrincipal>.Ignored)).Returns(Task.FromResult<IdentityUser>(null));

            // Act
            var result = await SUT.AddFavoriteLocation(new FavoriteLocationRequest { LocationName = "test"});

            // Assert
            var badResult = Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async void FavoriteLocationsController_AddFavoriteLocation_WhenAllDataFine()
        {
            // Arrange
            A.CallTo(() => _userManager.GetUserAsync(A<ClaimsPrincipal>.Ignored)).Returns(Task.FromResult<IdentityUser>(new IdentityUser { Id = "test"}));
            
            // Act
            var result = await SUT.AddFavoriteLocation(new FavoriteLocationRequest { LocationName = "test" });

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void FavoriteLocationsController_GetFavoriteLocations_WhenUserIsNull()
        {
            // Arrange
            A.CallTo(() => _userManager.GetUserAsync(A<ClaimsPrincipal>.Ignored)).Returns(Task.FromResult<IdentityUser>(null));

            // Act
            var result = await SUT.GetFavoriteLocations();

            // Assert
            var badResult = Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async void FavoriteLocationsController_GetFavoriteLocations_WhenUserIsNotNull()
        {
            // Arrange
            A.CallTo(() => _userManager.GetUserAsync(A<ClaimsPrincipal>.Ignored)).Returns(Task.FromResult<IdentityUser>(new IdentityUser { Id = "test" }));

            // Act
            var result = await SUT.GetFavoriteLocations();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void FavoriteLocationsController_RemoveFavoriteLocations_WhenUserIsNull()
        {
            // Arrange
            A.CallTo(() => _userManager.GetUserId(A<ClaimsPrincipal>.Ignored)).Returns(null);

            // Act
            var result = await SUT.RemoveFavoriteLocation("test");

            // Assert
            var badResult = Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async void FavoriteLocationsController_RemoveFavoriteLocations_WhenLocationDoesntExist()
        {
            // Arrange
            A.CallTo(() => _userManager.GetUserId(A<ClaimsPrincipal>.Ignored)).Returns("test");

            // Act
            var result = await SUT.RemoveFavoriteLocation("test");

            // Assert
            var badResult = Assert.IsType<NotFoundObjectResult>(result);
        }

        //[Fact]
        //public async void FavoriteLocationsController_RemoveFavoriteLocations_WhenAllDataIsFine()
        //{
        //    // Arrange
        //    var testingLocation = new FavouriteLocation
        //    {
        //        Name = "test",
        //        UserId = "test" 
        //    };
        //    A.CallTo(() => _userManager.GetUserId(A<ClaimsPrincipal>.Ignored)).Returns("test");
        //    _context.FavoriteLocations.Add(testingLocation);
        //    // Act
        //    var result = await SUT.RemoveFavoriteLocation("test");

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //}

        [Fact]
        public async void FavoriteLocationsController_GetWeatherForFavorites_WhenUserIsNull()
        {
            // Arrange
            A.CallTo(() => _userManager.GetUserAsync(A<ClaimsPrincipal>.Ignored)).Returns(Task.FromResult<IdentityUser>(null));

            // Act
            var result = await SUT.GetWeatherForFavorites();

            // Assert
            var badResult = Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async void FavoriteLocationsController_GetWeatherForFavorites_WhenUserIsNotNull()
        {
            // Arrange
            A.CallTo(() => _userManager.GetUserAsync(A<ClaimsPrincipal>.Ignored)).Returns(Task.FromResult<IdentityUser>(new IdentityUser { Id = "test" }));

            // Act
            var result = await SUT.GetWeatherForFavorites();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }
    }
}
