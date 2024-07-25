using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using TuiFly.Domain.Models.Configuration;

namespace TuiFly.Api.Configurations
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var swaggerSettings = new SwaggerSettings();
            configuration.GetSection("Swagger").Bind(swaggerSettings);

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc(swaggerSettings.Version, new OpenApiInfo
                {
                    Version = swaggerSettings.Version,
                    Title = swaggerSettings.Title,
                    Description = swaggerSettings.Description,
                });

                s.AddSecurityDefinition(swaggerSettings.Bearer.Scheme, new OpenApiSecurityScheme
                {
                    Description = swaggerSettings.Bearer.Description,
                    Name = swaggerSettings.Bearer.Name,
                    Scheme = swaggerSettings.Bearer.Scheme,
                    BearerFormat = swaggerSettings.Bearer.BearerFormat,
                    In = Enum.Parse<ParameterLocation>(swaggerSettings.Bearer.In, true),
                    Type = Enum.Parse<SecuritySchemeType>(swaggerSettings.Bearer.Type, true)
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = swaggerSettings.Bearer.Scheme
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            var swaggerSettings = new SwaggerSettings();
            configuration.GetSection("Swagger").Bind(swaggerSettings);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerSettings.Endpoint, swaggerSettings.Version);
            });
        }
    }
}
