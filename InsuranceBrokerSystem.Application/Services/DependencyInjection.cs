



namespace InsuranceBrokerSystem.Application.Services
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            MappingConfig.Configure();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly));
            
            // JWT and Authentication services
            services.AddScoped<InsuranceBrokerSystem.Application.Interfaces.IJwtService, JwtService>();
            services.AddScoped<InsuranceBrokerSystem.Application.Interfaces.IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
