using AuthService.Data;
using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteLocationsController : ControllerBase
    {
        private readonly AuthDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly WeatherDashboardClient _weatherDashboardClient;

        public FavoriteLocationsController(AuthDbContext context, UserManager<IdentityUser> userManager, WeatherDashboardClient weatherDashboardClient)
        {
            _context = context;
            _userManager = userManager;
            _weatherDashboardClient = weatherDashboardClient;
        }

        public class FavoriteLocationRequest
        {
            public string LocationName { get; set; }
        }

        [HttpPost("add-to-favourites")]
        public async Task<IActionResult> AddFavoriteLocation([FromBody] FavoriteLocationRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.LocationName))
                return BadRequest("Location name is required.");

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var favorite = new FavouriteLocation
            {
                Name = request.LocationName,
                UserId = user.Id
            };

            _context.FavoriteLocations.Add(favorite);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Location added to favorites." });
        }


        [HttpGet("get-favourites")]
        public async Task<IActionResult> GetFavoriteLocations()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var favorites = await _context.FavoriteLocations
                .Where(f => f.UserId == user.Id)
                .Select(f => f.Name)
                .ToListAsync();

            return Ok(favorites);
        }

        [HttpDelete("remove/{locationName}")]
        public async Task<IActionResult> RemoveFavoriteLocation(string locationName)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized(new { Message = "User not authenticated" });
            }

            var favoriteLocation = await _context.FavoriteLocations
                .FirstOrDefaultAsync(fl => fl.UserId == userId && fl.Name == locationName);

            if (favoriteLocation == null)
            {
                return NotFound(new { Message = $"Location {locationName} not found in favorites" });
            }

            _context.FavoriteLocations.Remove(favoriteLocation);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Location {locationName} removed from favorites" });
        }

        [HttpGet("weather")]
        public async Task<IActionResult> GetWeatherForFavorites()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var favorites = await _context.FavoriteLocations
                .Where(f => f.UserId == user.Id)
                .Select(f => f.Name)
                .ToListAsync();

            var weatherData = new List<object>();
            foreach (var location in favorites)
            {
                try
                {
                    var weather = await _weatherDashboardClient.GetWeatherDataForLocation(location);
                    weatherData.Add(new { Location = location, Weather = weather });
                }
                catch (Exception ex)
                {
                    weatherData.Add(new { Location = location, Error = ex.Message });
                }
            }

            return Ok(weatherData);
        }
    }
}
