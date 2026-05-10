
using Azure;
using InsuranceBrokerSystem.Application.DTOs.Master_Table.Bank;
using InsuranceBrokerSystem.Application.Features.Banks.Queries.GetAllBanks;
using InsuranceBrokerSystem.Application.Mediators;
using InsuranceBrokerSystem.Application.Services;
using InsuranceBrokerSystem.Infrastructure;
using LiveChartsCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InsuranceBrokerSystem.Api
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();

            builder.Services.AddScoped<IManualMediator, ManualMediator>();
            builder.Services.Scan(scan => scan
                .FromAssemblies(typeof(GetAllBanksHandler).Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IManualRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            // Add JWT Authentication
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"]!;
            var issuer = jwtSettings["Issuer"]!;
            var audience = jwtSettings["Audience"]!;

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.WithOrigins("http://localhost:4201", "https://localhost:4201")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            // Use CORS
            app.UseCors("AllowAngularApp");
            
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            // Initialize database with seed data
            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<InsuranceBrokerSystem.Infrastructure.Data.DbInitializer>();
                await dbInitializer.InitializeAsync();
            }

            app.Run();
        }
    }
}
