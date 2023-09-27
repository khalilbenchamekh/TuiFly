using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TuiFly.Api.Controllers;
using TuiFly.Domain.Interfaces.Service.Flight;
using TuiFly.Domain.Interfaces.Service.Seed;
using TuiFly.Domain.Models.Response;
using TuiFly.Domain.Models.ViewModels;
using Xunit;

namespace TuiFly.Api.UnitTests.Controllers
{
    public class FlightControllerTests
    {
        [Fact]
        public async Task Save_ValidModel_ReturnsOkResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<FlightController>>();
            var _loggerFactoryMock = new Mock<ILoggerFactory>();
            _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(loggerMock.Object);

            var flightServiceMock = new Mock<IFlightService>();
            flightServiceMock.Setup(service => service.save(It.IsAny<ReservationViewModel>()))
                .ReturnsAsync(true); // Simulate a successful save operation

            var controller = new FlightController(_loggerFactoryMock.Object, flightServiceMock.Object);
            // Act
            var passengers = new List<PassengerViewModel>
            {
                new PassengerViewModel { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new PassengerViewModel { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
            };
            var model = new ReservationViewModel() { FlightId = Guid.NewGuid(), Passengers = passengers };
            var result = await controller.save(model);

            // Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);

            var response = objectResult.Value as MainResponse<bool>;
            Assert.Equal("success", response.Message);
        }

        [Fact]
        public async Task FindAll_ValidModel_ReturnsOkResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<FlightController>>();
            var _loggerFactoryMock = new Mock<ILoggerFactory>();
            _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(loggerMock.Object);
            var flightServiceMock = new Mock<IFlightService>();
            var flightResponses = new List<FlightResponse> { /* Add some sample responses */ };
            flightServiceMock.Setup(service => service.findAll(It.IsAny<FlightViewModel>()))
                .ReturnsAsync(flightResponses);

            var controller = new FlightController(_loggerFactoryMock.Object, flightServiceMock.Object);
            // Act
            var model = new FlightViewModel();
            var result = await controller.findAll(model);
            // Assert
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<MainResponse<FlightResponse>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<MainResponse<IEnumerable<FlightResponse>>>(okResult.Value);
            var response = okResult.Value as MainResponse<IEnumerable<FlightResponse>>;
            Assert.Empty(response.Response);
            Assert.Equal("success", response.Message);
        }
        
    }
}
