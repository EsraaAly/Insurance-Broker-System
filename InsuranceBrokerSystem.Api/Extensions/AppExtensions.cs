namespace InsuranceBrokerSystem.Api.Extensions
{
    public static class AppExtensions
    {
        public static async Task UseWebApplicationWarmup(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                _ = context.Model;
                await context.Database.CanConnectAsync();

                TypeAdapterConfig<Account, GetAccountDTO>.NewConfig().Compile();

                Console.WriteLine("🚀 System Warm-up Completed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Warm-up Failed: {ex.Message}");
            }
        }
    }
}
