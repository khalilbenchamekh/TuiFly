using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NetDevPack.Data;
using NetDevPack.Mediator;
using TuiFly.Domain.Interfaces.Repository.Flight;
using TuiFly.Domain.Interfaces.Repository.Seed;
using TuiFly.Domain.Models.Entities;
using TuiFly.Infra.Data.Context;
using TuiFly.Infra.Data.Repository.Seed;
using Xunit;

namespace TuiFly.Infra.Data.UnitTest.Repository.Seed
{
    public class InitSeedRepositoryTests
    {
        private readonly Mock<ILogger<InitSeedRepository>> _loggerMock;
        private readonly Mock<IFlightRepository> _flightRepositoryMock;
        private readonly TuiFlyContext _dbContext;
        private readonly IInitSeedRepository _initSeedRepository;
        private readonly Mock<ILoggerFactory> _loggerFactoryMock;

        public InitSeedRepositoryTests()
        {
            // Arrange
            _loggerMock = new Mock<ILogger<InitSeedRepository>>();
            _loggerFactoryMock = new Mock<ILoggerFactory>();
            _flightRepositoryMock = new Mock<IFlightRepository>();
            // Setup the LoggerFactory to return the Logger
            _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(_loggerMock.Object);
            // Set up an in-memory database for testing
            var options = new DbContextOptionsBuilder<TuiFlyContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            // Create a mock of IMediatorHandler using Moq
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            _dbContext = new TuiFlyContext(options, mediatorHandlerMock.Object);

            // Create a mock for IUnitOfWork
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            // Configure the _flightRepository mock to return the unitOfWorkMock
            _flightRepositoryMock.Setup(x => x.UnitOfWork).Returns(unitOfWorkMock.Object);
            _initSeedRepository = new InitSeedRepository(_loggerFactoryMock.Object, _dbContext, _flightRepositoryMock.Object);
        }

        [Fact]
        public async Task CreatePlan_Should_Save_Plans()
        {
            // Act
            var savedPlans = await _initSeedRepository.CreatePlan();

            // Assert
            Assert.NotNull(savedPlans);
            Assert.NotEmpty(savedPlans);
            Assert.Equal(2, savedPlans.Count);
        }

        [Fact]
        public async Task CreateArrangement_Should_Create_Arrangements_For_Flight()
        {
            // Arrange
            var flight = new FlightEntity
            {
                Id = Guid.NewGuid(), // Replace with flight details as needed
            };

            // Act
            await _initSeedRepository.CreateArrangement(flight);

            // Assert
            var seatArrangements = _dbContext.SeatArrangements.ToList();
            Assert.NotNull(seatArrangements);
            Assert.NotEmpty(seatArrangements);
        }

        [Fact]
        public async Task GenerateFlightsAsync_Should_Generate_Flights_For_Plan()
        {
            // Arrange
            var plan = new PlanEntity
            {
                Id = Guid.NewGuid(), // Replace with plan details as needed
            };
            const int numberOfFlights = 5; // Adjust as needed

            // Act
            var generatedFlights = await _initSeedRepository.GenerateFlightsAsync(plan, numberOfFlights);

            // Assert
            Assert.NotNull(generatedFlights);
            Assert.NotEmpty(generatedFlights);
            Assert.Equal(numberOfFlights, generatedFlights.Count);
        }
    }
}
