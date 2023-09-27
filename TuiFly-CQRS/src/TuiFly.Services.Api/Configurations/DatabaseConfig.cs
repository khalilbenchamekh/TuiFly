using Microsoft.EntityFrameworkCore;
using TuiFly.Domain.Interfaces.Service.Seed;
using TuiFly.Infra.Data.Context;

namespace TuiFly.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<TuiFlyContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("TuiFly.Infra.Data")));
        }

        public static void InitializeDbTestDataAsync(this IApplicationBuilder app)
        {
            using (var serviceScope = app?.ApplicationServices?.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                if (serviceScope != null)
                {
                    serviceScope.ServiceProvider.GetRequiredService<TuiFlyContext>().Database.Migrate();
                    var context = serviceScope.ServiceProvider.GetRequiredService<TuiFlyContext>();
                    context.Database.EnsureCreated();
                    if (!context.Plans.Any())
                    {
                        var initSeed = serviceScope.ServiceProvider.GetRequiredService<IInitSeedService>();
                        initSeed.Seed().Wait();
                    }
                }
            }
        }
    }
}