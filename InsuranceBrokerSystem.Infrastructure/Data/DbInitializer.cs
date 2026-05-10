using InsuranceBrokerSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using InsuranceBrokerSystem.Application.Interfaces;

namespace InsuranceBrokerSystem.Infrastructure.Data
{
    public class DbInitializer
    {
        private readonly AppDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(AppDbContext dbContext, IPasswordHasher passwordHasher, ILogger<DbInitializer> logger)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            try
            {
                // Check if database has been created
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                // Create default roles if they don't exist
                await CreateDefaultRolesAsync();

                // Create default admin user if it doesn't exist
                await CreateDefaultAdminUserAsync();

                _logger.LogInformation("Database initialization completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during database initialization.");
                throw;
            }
        }

        private async Task CreateDefaultRolesAsync()
        {
            var roles = new[]
            {
                new Role { Name = "Admin", Description = "System administrator with full access" },
                new Role { Name = "Manager", Description = "Manager with limited administrative access" },
                new Role { Name = "User", Description = "Regular user with basic access" }
            };

            foreach (var role in roles)
            {
                var existingRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == role.Name);
                if (existingRole == null)
                {
                    await _dbContext.Roles.AddAsync(role);
                    _logger.LogInformation($"Created role: {role.Name}");
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task CreateDefaultAdminUserAsync()
        {
            var adminRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
            if (adminRole == null)
            {
                _logger.LogWarning("Admin role not found. Skipping admin user creation.");
                return;
            }

            var existingAdmin = await _dbContext.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Username == "admin");

            if (existingAdmin == null)
            {
                var adminUser = new User
                {
                    Username = "admin",
                    Email = "admin@ibs.com",
                    PasswordHash = _passwordHasher.HashPassword("Admin123!"),
                    FirstName = "System",
                    LastName = "Administrator",
                    IsActive = true,
                    LastLoginDate = null,
                    RefreshToken = null,
                    RefreshTokenExpiryDate = null
                };

                await _dbContext.Users.AddAsync(adminUser);
                await _dbContext.SaveChangesAsync();

                // Assign admin role to the user
                var userRole = new UserRole
                {
                    UserId = adminUser.Id,
                    RoleId = adminRole.Id
                };

                await _dbContext.UserRoles.AddAsync(userRole);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Created default admin user: admin");
            }
            else
            {
                // Ensure admin user has admin role
                var hasAdminRole = existingAdmin.UserRoles.Any(ur => ur.RoleId == adminRole.Id);
                if (!hasAdminRole)
                {
                    var userRole = new UserRole
                    {
                        UserId = existingAdmin.Id,
                        RoleId = adminRole.Id
                    };

                    await _dbContext.UserRoles.AddAsync(userRole);
                    await _dbContext.SaveChangesAsync();

                    _logger.LogInformation("Assigned admin role to existing admin user");
                }
            }
        }
    }
}
