using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Microsoft.Extensions.Configuration;
using Xunit;
using TuiFly.Api.Configurations;
using TuiFly.Domain.Models.Configuration;
using Microsoft.AspNetCore.Http;
namespace TuiFly.Api.UnitTests.Configuration
{
    public class CorsConfigTests
    {
        [Fact]
        public async Task AddCorsConfiguration_ShouldConfigureCorsPolicyAsync()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddLogging(); // Register the ILoggerFactory
            // Define your CORS configuration settings as a dictionary
            var corsConfiguration = new Dictionary<string, string>
            {
                { "CorsPolicy:AllowedOrigins:0", "http://example.com" },
                { "CorsPolicy:AllowedOrigins:1", "https://example2.com" },
            };

            // Create a custom IConfiguration instance using the dictionary
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(corsConfiguration)
                .Build();
            // Create a mock HttpContext
            var httpContextMock = new DefaultHttpContext();

            // Set up the HttpContextAccessor
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(a => a.HttpContext)
                .Returns(httpContextMock);
            // Act
            CorsConfig.AddCorsConfiguration(services, configuration);

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var corsOptions = serviceProvider.GetRequiredService<ICorsService>() as CorsService;
            var corsPolicyProvider = serviceProvider.GetRequiredService<ICorsPolicyProvider>();
            // Retrieve the CORS policy by name
            var policy = await corsPolicyProvider.GetPolicyAsync(httpContextMock, "default");
            Assert.NotNull(policy);
            Assert.Equal(corsConfiguration["CorsPolicy:AllowedOrigins:0"], policy.Origins[0]);
            Assert.Equal(corsConfiguration["CorsPolicy:AllowedOrigins:1"], policy.Origins[1]);
            Assert.True(policy.AllowAnyHeader);
            Assert.True(policy.AllowAnyMethod);
        }
    }
}