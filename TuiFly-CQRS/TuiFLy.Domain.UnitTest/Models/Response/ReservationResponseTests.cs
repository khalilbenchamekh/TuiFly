using TuiFly.Domain.Models.Response;
using TuiFly.Domain.Models.ViewModels;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Models.Response
{
    public class ReservationResponseTests
    {
        [Fact]
        public void Constructor_WithValidParameters_SetsProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var flightId = Guid.NewGuid();
            var numberOfPassengers = 2;
            var passengerName = "John Doe";
            var seatNumber = "A1";
            var dateReservation = DateTime.UtcNow;
            var passengers = new List<CustomerViewModel> { new CustomerViewModel() };

            // Act
            var reservationResponse = new ReservationResponse
            {
                Id = id,
                FlightId = flightId,
                NumberOfPassengers = numberOfPassengers,
                PassengerName = passengerName,
                SeatNumber = seatNumber,
                DateReservation = dateReservation,
                Passengers = passengers
            };

            // Assert
            Assert.Equal(id, reservationResponse.Id);
            Assert.Equal(flightId, reservationResponse.FlightId);
            Assert.Equal(numberOfPassengers, reservationResponse.NumberOfPassengers);
            Assert.Equal(passengerName, reservationResponse.PassengerName);
            Assert.Equal(seatNumber, reservationResponse.SeatNumber);
            Assert.Equal(dateReservation, reservationResponse.DateReservation);
            Assert.Equal(passengers, reservationResponse.Passengers);
        }

        [Fact]
        public void Constructor_WithDefaultValues_SetsProperties()
        {
            // Arrange

            // Act
            var reservationResponse = new ReservationResponse();

            // Assert
            Assert.Equal(Guid.Empty, reservationResponse.Id);
            Assert.Equal(Guid.Empty, reservationResponse.FlightId);
            Assert.Equal(0, reservationResponse.NumberOfPassengers);
            Assert.Null(reservationResponse.PassengerName);
            Assert.Null(reservationResponse.SeatNumber);
            Assert.Equal(default(DateTime), reservationResponse.DateReservation);
            Assert.Null(reservationResponse.Passengers);
        }
    }
}
