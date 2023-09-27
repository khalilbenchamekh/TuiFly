using TuiFly.Domain.Models.Response;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Models.Response
{
    public class MainResponseTests
    {
        [Fact]
        public void Constructor_WithValidParameters_SetsProperties()
        {
            // Arrange
            var responseValue = 42;
            var message = "Success";

            // Act
            var mainResponse = new MainResponse<int>
            {
                Response = responseValue,
                Message = message
            };

            // Assert
            Assert.Equal(responseValue, mainResponse.Response);
            Assert.Equal(message, mainResponse.Message);
            Assert.False(mainResponse.Error);
        }

        [Fact]
        public void Constructor_WithDefaultValues_SetsProperties()
        {
            // Arrange

            // Act
            var mainResponse = new MainResponse<int>();

            // Assert
            Assert.Equal(default(int), mainResponse.Response);
            Assert.Equal(string.Empty, mainResponse.Message);
            Assert.False(mainResponse.Error);
        }

        [Fact]
        public void Constructor_WithError_SetsErrorProperty()
        {
            // Arrange

            // Act
            var mainResponse = new MainResponse<int>
            {
                Error = true
            };

            // Assert
            Assert.True(mainResponse.Error);
        }
    }
}
