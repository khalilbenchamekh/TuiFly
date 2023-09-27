using TuiFly.Domain.Models.Entities;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Models.Entities
{
    public class SeatArrangementEntityTests
    {
        [Fact]
        public void Constructor_WithValidParameters_SetsPropertiesCorrectly()
        {
            // Arrange
            var flightId = Guid.NewGuid();
            var seatNumber = "A1";
            var status = true;

            // Act
            var seatArrangement = new SeatArrangementEntity
            {
                FlightId = flightId,
                SeatNumber = seatNumber,
                Status = status
            };

            // Assert
            Assert.Equal(flightId, seatArrangement.FlightId);
            Assert.Equal(seatNumber, seatArrangement.SeatNumber);
            Assert.Equal(status, seatArrangement.Status);
        }

        [Fact]
        public void Constructor_DefaultStatusIsTrue()
        {
            // Arrange
            var flightId = Guid.NewGuid();
            var seatNumber = "A1";

            // Act
            var seatArrangement = new SeatArrangementEntity
            {
                FlightId = flightId,
                SeatNumber = seatNumber
            };

            // Assert
            Assert.True(seatArrangement.Status);
        }

        [Fact]
        public void SetStatus_ToFalse_SetsStatusCorrectly()
        {
            // Arrange
            var seatArrangement = new SeatArrangementEntity();

            // Act
            seatArrangement.Status = false;

            // Assert
            Assert.False(seatArrangement.Status);
        }

        [Fact]
        public void Equals_WithDifferentObjects_ReturnsFalse()
        {
            // Arrange
            var seatArrangement1 = new SeatArrangementEntity
            {
                FlightId = Guid.NewGuid(),
                SeatNumber = "A1",
                Status = true
            };

            var seatArrangement2 = new SeatArrangementEntity
            {
                FlightId = Guid.NewGuid(), // Different FlightId
                SeatNumber = seatArrangement1.SeatNumber,
                Status = seatArrangement1.Status
            };

            // Act
            var areEqual = seatArrangement1.Equals(seatArrangement2);

            // Assert
            Assert.False(areEqual);
        }
    }
}
