using InsuranceBrokerSystem.Application.DTOs.Auth;
using InsuranceBrokerSystem.Domain.Entities;

namespace InsuranceBrokerSystem.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(UserDTO user);
        string GenerateRefreshToken();
        Task<LoginResponseDTO> AuthenticateUserAsync(LoginRequestDTO loginRequest);
        Task<LoginResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO refreshTokenRequest);
        Task<bool> ValidateTokenAsync(string token);
    }

    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int id);
        Task<List<Role>> GetUserRolesAsync(int userId);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> ExistsByUsernameAsync(string username);
        Task<bool> ExistsByEmailAsync(string email);
    }

    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }
}
