using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserLogin.Data.Models;
using UserLogin.Services;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace UserLogin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _authService.RegisterAsync(model);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            else
            return Ok(new { message = "Registration successful! Please check your email to confirm your account."});
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _authService.LoginAsync(model);
            if (result.Succeeded)
            {
                var user = await _authService.GetUserByNameAsync(model.Identifier);
                var token = await _authService.GenerateJwtTokenAsync(user);
                return Ok(new { message = "Login successful!", token });
            }
            return Unauthorized("Invalid login attempt.");
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _authService.ConfirmEmailAsync(userId, token);
            if (!result.Succeeded)
                return BadRequest("Email confirmation failed.");
            return Ok("Email confirmed successfully!");
        }
    }
}
