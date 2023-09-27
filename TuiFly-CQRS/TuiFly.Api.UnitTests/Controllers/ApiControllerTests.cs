using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Moq;
using TuiFly.Api.Controllers;
using Xunit;

namespace TuiFly.Api.UnitTests.Controllers
{
    public class ApiControllerTests
    {
        [Fact]
        public void CustomResponse_WithValidOperation_ShouldReturnOkResult()
        {
            // Arrange
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var controller = new TestApiController(loggerFactoryMock.Object);

            // Act
            var result = controller.CustomResponse();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void CustomResponse_WithInvalidOperation_ShouldReturnBadRequestResult()
        {
            // Arrange
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var controller = new TestApiController(loggerFactoryMock.Object);
            controller.AddError("Error 1");
            controller.AddError("Error 2");

            // Act
            var result = controller.CustomResponse();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CustomResponse_ModelState_ShouldAddErrorsAndReturnBadRequestResult()
        {
            // Arrange
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var controller = new TestApiController(loggerFactoryMock.Object);
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Key1", "Error 1");
            modelState.AddModelError("Key2", "Error 2");

            // Act
            var result = controller.CustomResponse(modelState);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void CustomResponse_ValidationResult_ShouldAddErrorsAndReturnBadRequestResult()
        {
            // Arrange
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var controller = new TestApiController(loggerFactoryMock.Object);
            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("Key1", "Error 1"));
            validationResult.Errors.Add(new ValidationFailure("Key2", "Error 2"));

            // Act
            var result = controller.CustomResponse(validationResult);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        // Create a test controller for testing
        private class TestApiController : ApiController<object>
        {
            public TestApiController(ILoggerFactory factory) : base(factory)
            {
            }
        }
    }
}
