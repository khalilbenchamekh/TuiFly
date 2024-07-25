using TuiFly.Domain.Models.Configuration;

namespace TuiFly.Api.Configurations;

public static class LocalizationConfig
{
    public static void ConfigureLocalization(this IServiceCollection services, IConfiguration configuration)
    {
        var localizationSettings = configuration.GetSection("Localization").Get<LocalizationConfiguration>();

        var supportedCultures = localizationSettings.SupportedCultures;

        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(localizationSettings.DefaultCulture)
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        services.AddSingleton(localizationOptions);
    }

    public static void UseLocalization(this IApplicationBuilder app)
    {
        var localizationOptions = app.ApplicationServices.GetRequiredService<RequestLocalizationOptions>();
        app.UseRequestLocalization(localizationOptions);
    }
}