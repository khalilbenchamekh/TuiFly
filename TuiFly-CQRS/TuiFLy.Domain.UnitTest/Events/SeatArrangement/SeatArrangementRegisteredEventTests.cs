using TuiFly.Domain.Events.Reservation;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Events.SeatArrangement
{
    public class SeatArrangementRegisteredEventTests
    {
        [Fact]
        public void Constructor_WithValidParameters_SetsProperties()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string seatNumber = "A1";
            bool status = true;
            Guid flightId = Guid.NewGuid();

            // Act
            var seatArrangementEvent = new SeatArrangementRegisteredEvent(id, seatNumber, status, flightId);

            // Assert
            Assert.Equal(id, seatArrangementEvent.Id);
            Assert.Equal(seatNumber, seatArrangementEvent.SeatNumber);
            Assert.Equal(status, seatArrangementEvent.Status);
            Assert.Equal(flightId, seatArrangementEvent.FlightId);
        }
    }
}
