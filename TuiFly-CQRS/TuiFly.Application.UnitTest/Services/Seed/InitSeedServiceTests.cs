using Microsoft.Extensions.Logging;
using Moq;
using TuiFly.Application.Services.Seed;
using TuiFly.Domain.Interfaces.Repository.Seed;
using TuiFly.Domain.Interfaces.Service.Seed;
using TuiFly.Domain.Models.Entities;
using Xunit;
namespace TuiFly.Application.UnitTest.Services.Seed
{
    public class InitSeedServiceTests
    {
        [Fact]
        public async Task Seed_SuccessfullyGeneratesPlansFlightsSeatArrangements()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IInitSeedService>>();
            var _loggerFactoryMock = new Mock<ILoggerFactory>();
            _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(loggerMock.Object);

            var initSeedRepositoryMock = new Mock<IInitSeedRepository>();
            var plans = new List<PlanEntity>
            {
                new PlanEntity
                {
                    Id = Guid.NewGuid(),
                    ModelName = "TestModel1"
                },
                new PlanEntity
                {
                    Id = Guid.NewGuid(),
                    ModelName = "TestModel2"
                },
            };

            var flights = new List<FlightEntity>
            {
                new FlightEntity
                {
                    Id = Guid.NewGuid(),
                    Airline = "TestAirline1",
                    FlightNumber = "F123",
                    PlanId = plans[0].Id
                },
                new FlightEntity
                {
                    Id = Guid.NewGuid(),
                    Airline = "TestAirline2",
                    FlightNumber = "F456",
                    PlanId = plans[1].Id
                },
            };
            var seatArrangements = new List<SeatArrangementEntity> { /* Initialize with test data */ };
            initSeedRepositoryMock.SetupSequence(repo => repo.CreatePlan())
                .ReturnsAsync(plans);
            initSeedRepositoryMock.Setup(repo => repo.GenerateFlightsAsync(It.IsAny<PlanEntity>(), It.IsAny<int>()))
                .ReturnsAsync(flights);
            initSeedRepositoryMock.Setup(repo => repo.CreateArrangement(It.IsAny<FlightEntity>()))
                .Returns(Task.CompletedTask);

            var initSeedService = new InitSeedService(_loggerFactoryMock.Object, initSeedRepositoryMock.Object);

            // Act
            await initSeedService.Seed();

            // Assert
            loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Information, // Expected LogLevel
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => true),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
            initSeedRepositoryMock.Verify(repo => repo.CreatePlan(), Times.Once);
            initSeedRepositoryMock.Verify(repo => repo.GenerateFlightsAsync(It.IsAny<PlanEntity>(), It.IsAny<int>()), Times.Exactly(2));
            initSeedRepositoryMock.Verify(repo => repo.CreateArrangement(It.IsAny<FlightEntity>()), Times.Exactly(4));
        }

        [Fact]
        public async Task Seed_HandlesExceptionAndLogsError()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IInitSeedService>>();
            var _loggerFactoryMock = new Mock<ILoggerFactory>();
            _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(loggerMock.Object);

            var initSeedRepositoryMock = new Mock<IInitSeedRepository>();
            initSeedRepositoryMock.Setup(repo => repo.CreatePlan())
                .ThrowsAsync(new Exception("Test exception"));

            var initSeedService = new InitSeedService(_loggerFactoryMock.Object, initSeedRepositoryMock.Object);

            // Act
            await initSeedService.Seed();

            // Assert
            loggerMock.Verify(logger => logger.Log(LogLevel.Information, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => true), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
            loggerMock.Verify(logger => logger.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => true), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
        }
    }
}
