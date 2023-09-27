using AutoMapper;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using NetDevPack.Mediator;
using TuiFly.Application.Services;
using TuiFly.Domain.Commands.Flight.Validation;
using TuiFly.Domain.Interfaces.Repository.Flight;
using TuiFly.Domain.Interfaces.Service.Flight;
using TuiFly.Domain.Interfaces.Service.Seed;
using TuiFly.Domain.Models.Entities;
using TuiFly.Domain.Models.Response;
using TuiFly.Domain.Models.ViewModels;
using Xunit;

namespace TuiFly.Application.UnitTest.Services
{
    public class FlightServiceTests
    {
        [Fact]
        public async Task FindAll_ReturnsMappedResponse()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IInitSeedService>>();
            var _loggerFactoryMock = new Mock<ILoggerFactory>();
            _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(loggerMock.Object);
            var mapperMock = new Mock<IMapper>();
            var flightRepositoryMock = new Mock<IFlightRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var flightService = new FlightService(_loggerFactoryMock.Object, mapperMock.Object, flightRepositoryMock.Object, mediatorHandlerMock.Object);

            var request = new FlightViewModel(); // Initialize with test data
            var expectedFlights = new List<FlightEntity>(); // Initialize with test data
            var expectedResponse = new List<FlightResponse>(); // Initialize with test data

            flightRepositoryMock.Setup(repo => repo.findAll(request))
                .ReturnsAsync(expectedFlights);
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<FlightResponse>>(expectedFlights))
                .Returns(expectedResponse);

            // Act
            var result = await flightService.findAll(request);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task Save_WithValidReservationViewModel_ReturnsTrue()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IInitSeedService>>();
            var _loggerFactoryMock = new Mock<ILoggerFactory>();
            _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(loggerMock.Object);
            var mapperMock = new Mock<IMapper>();
            var flightRepositoryMock = new Mock<IFlightRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var flightService = new FlightService(_loggerFactoryMock.Object, mapperMock.Object, flightRepositoryMock.Object, mediatorHandlerMock.Object);

            var request = new ReservationViewModel
            {
                FlightId = Guid.NewGuid(), // Initialize with a valid FlightId
                Passengers = new List<PassengerViewModel>
            {
            new PassengerViewModel
            {
                LastName = "Smith",
                FirstName = "John",
                Email = "john.smith@example.com"
            }
            }
            };
            var flight = new FlightEntity { DepartureCity = "CityA", ArrivalCity = "CityB", DepartureDate = DateTime.Now.Date };
            var commandResult = new ValidationResult(); // Initialize with a successful validation result
            flightRepositoryMock.Setup(repo => repo.findById(request.FlightId))
                .ReturnsAsync(flight);
            mapperMock.Setup(mapper => mapper.Map<MakeReservationCommandValidation>(request))
                .Returns(new MakeReservationCommandValidation()); // Initialize with valid test data
            mediatorHandlerMock.Setup(handler => handler.SendCommand(It.IsAny<MakeReservationCommandValidation>()))
                .ReturnsAsync(commandResult);

            // Act
            var result = await flightService.save(request);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Save_WithInvalidFlight_ReturnsFalse()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IInitSeedService>>();
            var _loggerFactoryMock = new Mock<ILoggerFactory>();
            _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(loggerMock.Object);
            var mapperMock = new Mock<IMapper>();
            var flightRepositoryMock = new Mock<IFlightRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var flightService = new FlightService(_loggerFactoryMock.Object, mapperMock.Object, flightRepositoryMock.Object, mediatorHandlerMock.Object);

            var request = new ReservationViewModel(); // Initialize with valid test data
            var flightId = Guid.NewGuid(); // Initialize with test data

            // Flight is not found (invalid scenario)
            flightRepositoryMock.Setup(repo => repo.findById(flightId))
                .ReturnsAsync(null as FlightEntity);

            // Act
            var result = await flightService.save(request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Save_WithCommandValidationErrors_ReturnsFalse()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IInitSeedService>>();
            var _loggerFactoryMock = new Mock<ILoggerFactory>();
            _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(loggerMock.Object);
            var mapperMock = new Mock<IMapper>();
            var flightRepositoryMock = new Mock<IFlightRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var flightService = new FlightService(_loggerFactoryMock.Object, mapperMock.Object, flightRepositoryMock.Object, mediatorHandlerMock.Object);

            var request = new ReservationViewModel(); // Initialize with valid test data
            var flightId = Guid.NewGuid(); // Initialize with test data

            var flight = new FlightEntity // Initialize with test data
            {
                Id = flightId
            };

            var commandResult = new ValidationResult();
            commandResult.Errors.Add(new ValidationFailure("ErrorField", "Error Message")); // Simulate validation error

            flightRepositoryMock.Setup(repo => repo.findById(flightId))
                .ReturnsAsync(flight);
            mapperMock.Setup(mapper => mapper.Map<MakeReservationCommandValidation>(request))
                .Returns(new MakeReservationCommandValidation()); // Initialize with valid test data
            mediatorHandlerMock.Setup(handler => handler.SendCommand(It.IsAny<MakeReservationCommandValidation>()))
                .ReturnsAsync(commandResult);

            // Act
            var result = await flightService.save(request);

            // Assert
            Assert.False(result);
        }

    }
}
