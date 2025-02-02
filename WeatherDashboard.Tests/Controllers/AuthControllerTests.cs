using AuthService.Controllers;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WeatherDashboard.Tests.Controllers
{
    public class AuthControllerTests
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
            A.CallTo(() => _userManagerMock.CreateAsync(A<IdentityUser>.Ignored, A<string>.Ignored)).Returns(Task.FromResult(IdentityResult.Success));

            //Act
            var result = await SUT.Register(new AuthService.Models.RegisterModel());

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AuthController_Register_WhenRegistrationFails()
        {
            // Arrange
            A.CallTo(() => _userManagerMock.CreateAsync(A<IdentityUser>.Ignored, A<string>.Ignored)).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "User creation failed" })));

            //Act
            var result = await SUT.Register(new AuthService.Models.RegisterModel());

            //Assert
            var errorResult = Assert.IsType<BadRequestObjectResult>(result);
            var errors = errorResult.Value as IEnumerable<IdentityError>;
            errors.Should().Contain(e => e.Description == "User creation failed");
        }

        [Fact]
        public async Task AuthController_Login_WhenLoginSucceeds()
        {
            // Arrange
            A.CallTo(() => _signInManagerMock.PasswordSignInAsync(A<string>.Ignored, A<string>.Ignored, false, false)).Returns(Task.FromResult(SignInResult.Success));
            A.CallTo(() => _userManagerMock.FindByEmailAsync(A<string>.Ignored)).Returns(Task.FromResult(new IdentityUser { Id = Guid.NewGuid().ToString(), Email = "test@test.com" }));
            A.CallTo(() => _configurationMock["Jwt:Key"]).Returns("A1B2C3D4E5F60718293A4B5C6D7E8F90");
            A.CallTo(() => _configurationMock["Jwt:Issuer"]).Returns("test");
            A.CallTo(() => _configurationMock["Jwt:Audience"]).Returns("test");
            A.CallTo(() => _configurationMock["Jwt:ExpireMinutes"]).Returns("2");

            //Act
            var result = await SUT.Login(new AuthService.Models.LoginModel());

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AuthController_Login_WhenLoginFails()
        {
            // Arrange
            A.CallTo(() => _signInManagerMock.PasswordSignInAsync(A<string>.Ignored, A<string>.Ignored, false, false)).Returns(Task.FromResult(SignInResult.NotAllowed));
            
            //Act
            var result = await SUT.Login(new AuthService.Models.LoginModel());

            //Assert
            var errorResult = Assert.IsType<UnauthorizedObjectResult>(result);
        }
    }
}
