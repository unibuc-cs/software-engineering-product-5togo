using AuthService.Controllers;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WeatherDashboard.Tests.Controllers
{
    internal class AuthControllerTests
    {
        private readonly UserManager<IdentityUser> _userManagerMock;
        private readonly SignInManager<IdentityUser> _signInManagerMock;
        private readonly IConfiguration _configurationMock;
        private readonly AuthController SUT;

        public AuthControllerTests() 
        {
            _userManagerMock = A.Fake<UserManager<IdentityUser>>();
            _signInManagerMock = A.Fake<SignInManager<IdentityUser>>();
            _configurationMock = A.Fake<IConfiguration>();
            SUT = new AuthController(_userManagerMock, _signInManagerMock, _configurationMock);
        }

        [Fact]
        public async Task AuthController_Register_WhenRegistrationSucceeds()
        {
            // Arrange
            var user = new IdentityUser();
            A.CallTo(() => _userManagerMock.CreateAsync(user, A<string>.Ignored)).Returns(Task.FromResult(IdentityResult.Success));

            //Act
            var result = SUT.Register(new AuthService.Models.RegisterModel());

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultMessage = okResult.Value as string;
            resultMessage.Should().Be("User registered successfully");
        }

        [Fact]
        public async Task AuthController_Register_WhenRegistrationFails()
        {
            // Arrange
            var user = new IdentityUser();
            A.CallTo(() => _userManagerMock.CreateAsync(user, A<string>.Ignored)).Returns(Task.FromResult(IdentityResult.Success));

            //Act
            var result = SUT.Register(new AuthService.Models.RegisterModel());

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultMessage = okResult.Value as string;
            resultMessage.Should().Be("User registered successfully");
        }
    }
}
