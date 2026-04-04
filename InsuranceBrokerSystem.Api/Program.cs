namespace InsuranceBrokerSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(InsuranceMappingProfile).Assembly);});
            builder.Services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(InsuranceLOBMappingProfile).Assembly); });
            builder.Services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(AccountMappingProfile).Assembly); });
            builder.Services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(AccountMappingProfileUI).Assembly); });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddScoped<IRepoInsuranceClass, RepoInsuranceClass>();
            builder.Services.AddScoped<IInsuranceClassService, InsuranceClassService>();
            builder.Services.AddScoped<IRepoInsuranceLOB, RepoInsuranceLOB>();
            builder.Services.AddScoped<IInsuranceLOBService, InsuranceLOBService>();
            builder.Services.AddScoped<IRepoInsuranceCompany, RepoInsuranceCompany>();
            builder.Services.AddScoped<IRepoInsuranceContact, RepoInsuranceContact>();
            builder.Services.AddScoped<IRepoInsuranceProduct, RepoInsuranceProduct>();
            builder.Services.AddScoped<IInsuranceCompanyService, InsuranceCompanyService>();
            builder.Services.AddScoped<IInsuranceContractService, InsuranceContractService>();
            builder.Services.AddScoped<IInsuranceProductService, InsuranceProductService>();
            builder.Services.AddScoped<IRepoAccountNumber, RepoAccountNumber>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IInsuranceCompanyAccountService, InsuranceCompanyAccountService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
