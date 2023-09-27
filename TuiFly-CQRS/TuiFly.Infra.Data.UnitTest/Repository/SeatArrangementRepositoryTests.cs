using Microsoft.EntityFrameworkCore;
using Moq;
using NetDevPack.Data;
using NetDevPack.Mediator;
using TuiFly.Domain.Interfaces.Repository.SeatArrangement;
using TuiFly.Infra.Data.Context;
using TuiFly.Infra.Data.Repository;
using Xunit;

namespace TuiFly.Infra.Data.UnitTest.Repository
{
    public class SeatArrangementRepositoryTests
    {
        private readonly TuiFlyContext _context;
        private readonly ISeatArrangementRepository _repository;
        private readonly Mock<ISeatArrangementRepository> _repositoryMock;

        public SeatArrangementRepositoryTests()
        {
            _repositoryMock = new Mock<ISeatArrangementRepository>();
            var options = new DbContextOptionsBuilder<TuiFlyContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Create a mock of IMediatorHandler using Moq
            var mediatorHandlerMock = new Mock<IMediatorHandler>();

            _context = new TuiFlyContext(options, mediatorHandlerMock.Object);

            // Create a mock for IUnitOfWork
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            // Configure the _repository mock to return the unitOfWorkMock
            _repositoryMock.Setup(x => x.UnitOfWork).Returns(unitOfWorkMock.Object);

            _repository = new SeatArrangementRepository(_context);
        }

        [Fact]
        public async Task GenerateFamilySeats_ValidFamilySize_ReturnsSeats()
        {
            // Arrange
            var familySize = 4; // Adjust family size as needed
            var availableSeats = new List<string> { "A1", "A2", "A3", "A4", "B1", "B2", "B3", "B4" }; // Adjust available seats
            var expectedSeatsCount = familySize;

            // Act
            var resultSeats = _repository.GenerateFamilySeats(familySize, availableSeats);

            // Assert
            Assert.Equal(expectedSeatsCount, resultSeats.Count);
        }

        [Fact]
        public async Task GenerateFamilySeats_InvalidFamilySize_ReturnsEmptyList()
        {
            // Arrange
            var familySize = 0; // Invalid family size
            var availableSeats = new List<string> { "A1", "A2", "A3", "A4", "B1", "B2", "B3", "B4" };

            // Act
            var resultSeats = _repository.GenerateFamilySeats(familySize, availableSeats);

            // Assert
            Assert.Empty(resultSeats);
        }

        [Fact]
        public async Task GetSeatArrangement_ValidInput_ReturnsSeatArrangements()
        {
            // Arrange
            var flightId = Guid.NewGuid(); // Replace with a valid flight ID
            var seats = new List<string> { "A1", "B2", "C3" }; // Adjust seat numbers as needed

            // Act
            var resultSeatArrangements = await _repository.GetSeatArrangement(flightId, seats);

            // Assert
            Assert.NotNull(resultSeatArrangements);
        }

        [Fact]
        public async Task GetAvailableSeats_ValidFlightId_ReturnsAvailableSeats()
        {
            // Arrange
            var flightId = Guid.NewGuid(); // Replace with a valid flight ID

            // Act
            var resultAvailableSeats = await _repository.GetAvailableSeats(flightId,10);

            // Assert
            Assert.NotNull(resultAvailableSeats);
        }

        [Fact]
        public void GetRange_ValidSeatNumber_ReturnsRange()
        {
            // Arrange
            var seatNumber = "A12"; // Replace with a valid seat number
            var expectedRange = 12; // Replace with the expected range

            // Act
            var resultRange = _repository.GetRange(seatNumber);

            // Assert
            Assert.Equal(expectedRange, resultRange);
        }

        [Fact]
        public void GetPosition_ValidSeatNumber_ReturnsPosition()
        {
            // Arrange
            var seatNumber = "D1"; // Replace with a valid seat number
            var expectedPosition = 'G'; // Replace with the expected position

            // Act
            var resultPosition = _repository.GetPosition(seatNumber);

            // Assert
            Assert.Equal(expectedPosition, resultPosition);
        }
    }
}
