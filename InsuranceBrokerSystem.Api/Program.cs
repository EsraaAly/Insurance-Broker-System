namespace InsuranceBrokerSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddValidatorsFromAssembly(
                    typeof(GenerateInsuranceCompanyAccountsValidator).Assembly);
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(GetAccountByIdQuery).Assembly);
            });

            builder.Services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(InsuranceMappingProfile).Assembly);});
            //builder.Services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(InsuranceLOBMappingProfile).Assembly); });
            //builder.Services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(AccountMappingProfile).Assembly); });
            //builder.Services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(AccountMappingProfileUI).Assembly); });
            //builder.Services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(ClientMappingProfile).Assembly); });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IInsuranceLOBRepository, InsuranceLOBRepository>();
            builder.Services.AddScoped<IInsuranceCompanyRepository, InsuranceCompanyRepository>();
            builder.Services.AddScoped<IInsuranceContactRepository, InsuranceContactRepository>();
            builder.Services.AddScoped<IInsuranceProductRepository, InsuranceProductRepository>();
            builder.Services.AddScoped<IAccountNumberRepository, AccountNumberRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();

                    context.Response.StatusCode = 500;

                    await context.Response.WriteAsJsonAsync(new
                    {
                        Message = error?.Error.Message,
                        StackTrace = error?.Error.StackTrace
                    });
                });
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
