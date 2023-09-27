using TuiFly.Domain.Models.Configuration;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Models.Configuration
{
    public class CorsConfigurationTests
    {
        [Fact]
        public void AllowedOrigins_SetAndGetCorrectly()
        {
            // Arrange
            var corsConfig = new CorsConfiguration();
            var expectedOrigins = new[] { "http://example.com", "https://example2.com" };

            // Act
            corsConfig.AllowedOrigins = expectedOrigins;
            var actualOrigins = corsConfig.AllowedOrigins;

            // Assert
            Assert.Equal(expectedOrigins, actualOrigins);
        }

        [Fact]
        public void AllowedOrigins_DefaultValueIsNull()
        {
            // Arrange
            var corsConfig = new CorsConfiguration();

            // Act
            var allowedOrigins = corsConfig.AllowedOrigins;

            // Assert
            Assert.Null(allowedOrigins);
        }

        [Fact]
        public void AllowedOrigins_CanBeSetToEmptyArray()
        {
            // Arrange
            var corsConfig = new CorsConfiguration();
            var expectedOrigins = new string[0];

            // Act
            corsConfig.AllowedOrigins = expectedOrigins;
            var actualOrigins = corsConfig.AllowedOrigins;

            // Assert
            Assert.Equal(expectedOrigins, actualOrigins);
        }
    }
}
