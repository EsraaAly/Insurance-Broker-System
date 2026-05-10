using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using InsuranceBrokerSystem.Application.DTOs.Auth;
using InsuranceBrokerSystem.Application.Interfaces;
using InsuranceBrokerSystem.Domain.Entities;

namespace InsuranceBrokerSystem.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;

        public JwtService(IConfiguration configuration, IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public string GenerateJwtToken(UserDTO user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"]!;
            var issuer = jwtSettings["Issuer"]!;
            var audience = jwtSettings["Audience"]!;
            var expirationMinutes = Convert.ToInt32(jwtSettings["ExpirationMinutes"] ?? "60");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add roles as claims
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<LoginResponseDTO> AuthenticateUserAsync(LoginRequestDTO loginRequest)
        {
            var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);
            
            if (user == null)
                throw new UnauthorizedAccessException("Invalid username or password");

            if (!_passwordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid username or password");

            // Update last login date
            user.LastLoginDate = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            var userRoles = await _userRepository.GetUserRolesAsync(user.Id);
            var userDto = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = userRoles.Select(r => r.Name).ToList()
            };

            var token = GenerateJwtToken(userDto);
            var refreshToken = GenerateRefreshToken();

            // Store refresh token in database
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateAsync(user);

            return new LoginResponseDTO
            {
                Token = token,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(60),
                User = userDto
            };
        }

        public async Task<LoginResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO refreshTokenRequest)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"]!;

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };

                var principal = tokenHandler.ValidateToken(refreshTokenRequest.Token, tokenValidationParameters, out SecurityToken validatedToken);
                
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                    throw new UnauthorizedAccessException("Invalid token");

                var user = await _userRepository.GetByIdAsync(Convert.ToInt32(userId));
                if (user == null || user.RefreshToken != refreshTokenRequest.RefreshToken || 
                    user.RefreshTokenExpiryDate < DateTime.UtcNow)
                    throw new UnauthorizedAccessException("Invalid refresh token");

                var userRoles = await _userRepository.GetUserRolesAsync(user.Id);
                var userDto = new UserDTO
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = userRoles.Select(r => r.Name).ToList()
                };

                var newToken = GenerateJwtToken(userDto);
                var newRefreshToken = GenerateRefreshToken();

                // Update refresh token in database
                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(7);
                await _userRepository.UpdateAsync(user);

                return new LoginResponseDTO
                {
                    Token = newToken,
                    RefreshToken = newRefreshToken,
                    Expiration = DateTime.UtcNow.AddMinutes(60),
                    User = userDto
                };
            }
            catch
            {
                throw new UnauthorizedAccessException("Invalid token");
            }
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"]!;

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };

                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
