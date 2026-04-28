
using Azure;
using InsuranceBrokerSystem.Application.DTOs.Master_Table.Bank;
using InsuranceBrokerSystem.Application.Features.Banks.Queries.GetAllBanks;
using InsuranceBrokerSystem.Application.Mediators;
using InsuranceBrokerSystem.Application.Services;
using InsuranceBrokerSystem.Infrastructure;
using LiveChartsCore;

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
            
            app.UseAuthorization();
            app.MapControllers();

            //await app.UseWebApplicationWarmup();

            app.Run();
        }
    }
}
