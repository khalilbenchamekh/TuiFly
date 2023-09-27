using TuiFly.Domain.Events.Reservation;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Events.Reservation
{
    public class ReservationRegisteredEventTests
    {
        [Fact]
        public void Constructor_WithValidParameters_SetsPropertiesCorrectly()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            int numberOfPassengers = 2;
            string seatNumber = "A1";
            DateTime dateReservation = DateTime.Now;
            Guid flightId = Guid.NewGuid();

            // Act
            var reservationEvent = new ReservationRegisteredEvent(id, numberOfPassengers, seatNumber, dateReservation, flightId);

            // Assert
            Assert.Equal(id, reservationEvent.Id);
            Assert.Equal(numberOfPassengers, reservationEvent.NumberOfPassengers);
            Assert.Equal(seatNumber, reservationEvent.SeatNumber);
            Assert.Equal(dateReservation, reservationEvent.DateReservation);
            Assert.Equal(flightId, reservationEvent.FlightId);
        }
    }
}
