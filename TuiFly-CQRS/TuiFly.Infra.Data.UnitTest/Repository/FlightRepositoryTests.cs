using Microsoft.EntityFrameworkCore;
using Moq;
using NetDevPack.Data;
using NetDevPack.Mediator;
using TuiFly.Domain.Interfaces.Repository.Flight;
using TuiFly.Domain.Models.Entities;
using TuiFly.Domain.Models.ViewModels;
using TuiFly.Infra.Data.Context;
using TuiFly.Infra.Data.Repository;
using Xunit;

namespace TuiFly.Infra.Data.UnitTest.Repository
{
    public class FlightRepositoryTests
    {

        private readonly TuiFlyContext _context;
        private readonly IFlightRepository _repository;
        private readonly Mock<IFlightRepository> _repositoryMock;

        public FlightRepositoryTests()
        {
            _repositoryMock = new Mock<IFlightRepository>();
            var options = new DbContextOptionsBuilder<TuiFlyContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            // Create a mock of IMediatorHandler using Moq
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            _context = new TuiFlyContext(options, mediatorHandlerMock.Object);
            // Create a mock for IUnitOfWork
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            // Configure the __repository mock to return the unitOfWorkMock
            _repositoryMock.Setup(x => x.UnitOfWork).Returns(unitOfWorkMock.Object);
            _repository = new FlightRepository(_context);
        }
        [Fact]
        public async Task FindAll_ReturnsFilteredFlights_WhenValidRequestProvided()
        {
            // Arrange
            var request = new FlightViewModel
            {
                DepartureCity = "CityA",
                ArrivalCity = "CityB",
                DepartureDate = DateTime.Now.Date,
                PageSize = 10,
                PageNumber = 1
            };

            var testFlights = new List<FlightEntity>
        {
            new FlightEntity { DepartureCity = "CityA", ArrivalCity = "CityB", DepartureDate = DateTime.Now.Date },
            new FlightEntity { DepartureCity = "CityA", ArrivalCity = "CityB", DepartureDate = DateTime.Now.Date }
        };

            await _context.AddRangeAsync(testFlights);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.findAll(request);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task FindAll_ReturnsEmptyList_WhenInvalidRequestProvided()
        {
            // Arrange
            var request = new FlightViewModel
            {
                DepartureCity = null, // Invalid request without required fields
                ArrivalCity = "CityB",
                DepartureDate = DateTime.Now.Date,
                PageSize = 10,
                PageNumber = 1
            };

            // Act
            var result = await _repository.findAll(request);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task FindById_ReturnsFlight_WhenValidIdProvided()
        {
            // Arrange
            var flightId = Guid.NewGuid();
            var testFlight = new FlightEntity { Id = flightId };
            await _context.AddAsync(testFlight);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.findById(flightId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(flightId, result.Id);
        }

        [Fact]
        public async Task FindById_ReturnsNull_WhenInvalidIdProvided()
        {
            // Arrange
            var invalidFlightId = Guid.NewGuid();
            // Act
            var result = await _repository.findById(invalidFlightId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_UpdatesFlightEntity()
        {
            // Arrange
            var testFlight = new FlightEntity { Id = Guid.NewGuid(), DepartureCity = "CityA" };
            await _context.AddAsync(testFlight);
            await _context.SaveChangesAsync();

            // Update the flight entity
            testFlight.DepartureCity = "CityB";

            // Act
            _repository.Update(testFlight);
            await _context.SaveChangesAsync();

            // Assert
            var updatedFlight = await _context.Flights.FirstOrDefaultAsync(f => f.Id == testFlight.Id);
            Assert.NotNull(updatedFlight);
            Assert.Equal("CityB", updatedFlight.DepartureCity);
        }

        [Fact]
        public async Task AddRange_AddsMultipleFlightEntities()
        {
            // Arrange

            var testFlights = new List<FlightEntity>
        {
            new FlightEntity { Id = Guid.NewGuid(), DepartureCity = "CityA" },
            new FlightEntity { Id = Guid.NewGuid(), DepartureCity = "CityB" }
        };

            // Act
            await _repository.AddRanger(testFlights);
            await _context.SaveChangesAsync();

            // Assert
            var addedFlights = await _context.Flights.ToListAsync();
            Assert.NotNull(addedFlights);
            Assert.Equal(2, addedFlights.Count);
        }
    }
}
