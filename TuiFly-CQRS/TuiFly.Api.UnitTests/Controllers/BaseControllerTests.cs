using Microsoft.Extensions.Logging;
using Moq;
using System.Reflection;
using TuiFly.Api.Controllers;
using Xunit;

namespace TuiFly.Api.UnitTests.Controllers
{
    public class BaseControllerTests
    {
        [Fact]
        public void BaseController_ShouldCreateLogger()
        {
            // Arrange
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var loggerMock = new Mock<ILogger<object>>();

            loggerFactoryMock
                .Setup(factory => factory.CreateLogger(It.IsAny<string>()))
                .Returns(loggerMock.Object);

            // Act
            var controller = new TestController(loggerFactoryMock.Object);

            // Assert
            Assert.NotNull(controller.Logger);
        }

        // Create a test controller for testing
        private class TestController : BaseController<object>
        {
            public TestController(ILoggerFactory factory) : base(factory)
            {
            }
        }
    }
}
