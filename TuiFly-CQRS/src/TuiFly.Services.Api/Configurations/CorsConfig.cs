using TuiFly.Domain.Models.Configuration;

namespace TuiFly.Api.Configurations
{
    public static class CorsConfig
    {
        public static void AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Bind CORS configuration from appsettings.json
            var corsConfig = configuration.GetSection("CorsPolicy").Get<CorsConfiguration>();
            // cors
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(corsConfig.AllowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }
        public static void UseCorsSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseCors("default");
        }
    }
}
