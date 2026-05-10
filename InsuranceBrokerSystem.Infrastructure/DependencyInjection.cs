namespace InsuranceBrokerSystem.Infrastructure
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"),
                sqlOptions => sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IInsuranceLOBRepository, InsuranceLOBRepository>();
            services.AddScoped<IInsuranceCompanyRepository, InsuranceCompanyRepository>();
            services.AddScoped<IInsuranceContactRepository, InsuranceContactRepository>();
            services.AddScoped<IInsuranceProductRepository, InsuranceProductRepository>();
            services.AddScoped<IAccountNumberRepository, AccountNumberRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            
            // Authentication services
            services.AddScoped<InsuranceBrokerSystem.Application.Interfaces.IUserRepository, Infrastructure.Repositories.Auth.UserRepository>();
            
            // Database initializer
            services.AddScoped<Infrastructure.Data.DbInitializer>();

            return services;
        }
    }
}
