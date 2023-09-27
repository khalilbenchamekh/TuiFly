using Microsoft.AspNetCore.Hosting;
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
            // Add the required services for API documentation
            services.AddLogging(); // Register the ILoggerFactory
                                   // Create a mock IWebHostEnvironment
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();

            // Add the mock IWebHostEnvironment to the services collection
            services.AddSingleton(mockWebHostEnvironment.Object);

            // Add the mock IWebHostEnvironment to the services collection
            services.AddControllers();

            // Add Swagger configuration
            services.AddSwaggerConfiguration();

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
