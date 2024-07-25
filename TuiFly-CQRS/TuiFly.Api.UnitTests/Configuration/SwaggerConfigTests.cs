using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Moq;
using Swashbuckle.AspNetCore.Swagger;
using TuiFly.Api.Configurations;
using Xunit;

namespace TuiFly.Api.UnitTests.Configuration
{
    public class SwaggerConfigTests
    {
        [Fact]
        public void AddSwaggerConfiguration_ShouldRegisterSwaggerServices()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddLogging(); // Register the ILoggerFactory
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            services.AddSingleton(mockWebHostEnvironment.Object);
            services.AddControllers();

            // Create in-memory configuration
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Swagger:Version", "v1" },
                    { "Swagger:Title", "TuiFly Project" },
                    { "Swagger:Description", "TuiFly API Swagger surface" },
                    { "Swagger:Endpoint", "/swagger/v1/swagger.json" },
                    { "Swagger:Bearer:Description", "Input the JWT like: Bearer {your token}" },
                    { "Swagger:Bearer:Name", "Authorization" },
                    { "Swagger:Bearer:Scheme", "Bearer" },
                    { "Swagger:Bearer:BearerFormat", "JWT" },
                    { "Swagger:Bearer:In", "Header" },
                    { "Swagger:Bearer:Type", "ApiKey" }
                })
                .Build();

            // Add Swagger configuration with the configuration parameter
            services.AddSwaggerConfiguration(configuration);

            // Act
            var serviceProvider = services.BuildServiceProvider();

            // Assert
            var swaggerGen = serviceProvider.GetRequiredService<ISwaggerProvider>();
            var swaggerDoc = swaggerGen.GetSwagger("v1");

            // Ensure that the SwaggerDoc contains the expected information
            Assert.NotNull(swaggerDoc);
            Assert.Equal("TuiFly Project", swaggerDoc.Info.Title);
            Assert.Equal("TuiFly API Swagger surface", swaggerDoc.Info.Description);

            // Check if security definitions are configured
            Assert.Contains(swaggerDoc.Components.SecuritySchemes, pair => pair.Key == "Bearer");
            var bearerScheme = swaggerDoc.Components.SecuritySchemes["Bearer"];
            Assert.Equal("Input the JWT like: Bearer {your token}", bearerScheme.Description);
            Assert.Equal("Authorization", bearerScheme.Name);
            Assert.Equal(SecuritySchemeType.ApiKey, bearerScheme.Type);
        }
    }
}
