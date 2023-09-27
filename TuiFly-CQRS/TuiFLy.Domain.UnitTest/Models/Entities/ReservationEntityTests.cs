using TuiFly.Domain.Models.Entities;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Models.Entities
{
    public class ReservationEntityTests
    {
        [Fact]
        public void Constructor_WithValidParameters_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var numberOfPassengers = 3;
            var seatNumber = "A1";
            var dateReservation = DateTime.Now;
            var flightId = Guid.NewGuid();
            var passengers = new List<CustomerEntity>
        {
            new CustomerEntity(),
            new CustomerEntity(),
            new CustomerEntity()
        };

            // Act
            var reservation = new ReservationEntity(); // Create an instance
            reservation.Id = id;
            reservation.NumberOfPassengers = numberOfPassengers;
            reservation.SeatNumber = seatNumber;
            reservation.DateReservation = dateReservation;
            reservation.FlightId = flightId;
            reservation.Passengers = passengers;

            // Assert
            Assert.Equal(id, reservation.Id);
            Assert.Equal(numberOfPassengers, reservation.NumberOfPassengers);
            Assert.Equal(seatNumber, reservation.SeatNumber);
            Assert.Equal(dateReservation, reservation.DateReservation);
            Assert.Equal(flightId, reservation.FlightId);
            Assert.Equal(passengers, reservation.Passengers);
        }

        [Fact]
        public void Constructor_WithDefaultValues_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.Empty;
            var numberOfPassengers = 0;
            var seatNumber = "";
            var dateReservation = DateTime.MinValue;
            var flightId = Guid.Empty;
            var passengers = new List<CustomerEntity>();

            // Act
            var reservation = new ReservationEntity(); // Create an instance
            reservation.Id = id;
            reservation.NumberOfPassengers = numberOfPassengers;
            reservation.SeatNumber = seatNumber;
            reservation.DateReservation = dateReservation;
            reservation.FlightId = flightId;
            reservation.Passengers = passengers;

            // Assert
            Assert.Equal(id, reservation.Id);
            Assert.Equal(numberOfPassengers, reservation.NumberOfPassengers);
            Assert.Equal(seatNumber, reservation.SeatNumber);
            Assert.Equal(dateReservation, reservation.DateReservation);
            Assert.Equal(flightId, reservation.FlightId);
            Assert.Equal(passengers, reservation.Passengers);
        }
    }
}
