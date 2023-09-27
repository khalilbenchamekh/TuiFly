using TuiFly.Domain.Core.Exceptions;
using Xunit;
namespace TuiFly.Domain.Core.UnitTest.Exceptions
{
    public class NotFoundExceptionTests
    {
        [Fact]
        public void Constructor_WithMessage_SetsMessageProperty()
        {
            // Arrange
            string message = "This is a test message.";

            // Act
            var exception = new NotFoundException(message);

            // Assert
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void Constructor_WithNullMessage_SetsMessageToNull()
        {
            // Arrange
            Exception? exception = null;

            // Act
            try
            {
                new NotFoundException(null);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Assert
            Assert.Null(exception);
        }
    }
}
