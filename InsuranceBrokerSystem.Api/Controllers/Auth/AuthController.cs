using Microsoft.AspNetCore.Mvc;
using InsuranceBrokerSystem.Application.DTOs.Auth;
using InsuranceBrokerSystem.Application.Services;
using InsuranceBrokerSystem.Application.Interfaces;

namespace InsuranceBrokerSystem.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            try
            {
                var result = await _jwtService.AuthenticateUserAsync(loginRequest);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO refreshTokenRequest)
        {
            try
            {
                var result = await _jwtService.RefreshTokenAsync(refreshTokenRequest);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("validate-token")]
        public async Task<IActionResult> ValidateToken([FromBody] string token)
        {
            try
            {
                var isValid = await _jwtService.ValidateTokenAsync(token);
                return Ok(new { isValid });
            }
            catch
            {
                return BadRequest(new { isValid = false });
            }
        }
    }
}
