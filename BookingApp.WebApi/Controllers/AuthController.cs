using BookingApp.Business.Operations.User;
using BookingApp.Business.Operations.User.Dtos;
using BookingApp.WebApi.Jwt;
using BookingApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = Microsoft.AspNetCore.Identity.Data.LoginRequest;
using RegisterRequest = BookingApp.WebApi.Models.RegisterRequest;

namespace BookingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService; // User service for handling user operations

        // Constructor accepting the user service via dependency injection
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // Endpoint for user registration
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            // Validate the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return bad request if model state is invalid
            }

            // Create DTO for adding user
            var addUserDto = new AddUserDto
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                BirthDate = request.BirthDate
            };

            // Add the user using the user service
            var result = await _userService.AddUser(addUserDto);

            // Check if user addition succeeded
            if (result.IsSucceed)
                return Ok(); // Return OK response if successful
            else
                return BadRequest(result.Message); // Return error message if not successful
        }

        // Endpoint for user login
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            // Validate the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return bad request if model state is invalid
            }

            // Attempt to log in the user
            var result = _userService.LoginUser(new LoginUserDto { Email = request.Email, Password = request.Password });

            // Check if login succeeded
            if (!result.IsSucceed)
                return BadRequest(result.Message); // Return error message if not successful

            var user = result.Data; // Get user data from the result

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>(); // Dependency Injection for configuration

            // Generate JWT token
            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!, // Secret key for JWT
                Issuer = configuration["Jwt:Issuer"]!, // Issuer for JWT
                Audience = configuration["Jwt:Audience"]!, // Audience for JWT
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!) // Token expiration time
            });

            // Return success response with token
            return Ok(new LoginResponse
            {
                Message = "Login successfully.", // Success message
                Token = token // JWT token
            });
        }

        // Endpoint to get the current user's information
        [HttpGet("me")]
        [Authorize] // Requires authorization
        public IActionResult GetMyUser()
        {
            return Ok(); // Return OK response (user information can be added here)
        }
    }
}
