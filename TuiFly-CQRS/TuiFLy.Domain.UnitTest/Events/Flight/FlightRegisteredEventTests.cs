using TuiFly.Domain.Events.Flight;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Events.Flight
{

    public class FlightRegisteredEventTests
    {
        [Fact]
        public void Constructor_WithValidParameters_SetsProperties()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string airline = "AirlineName";
            string flightNumber = "Flight123";
            string departureCity = "DepartureCity";
            string arrivalCity = "ArrivalCity";
            DateTime departureDate = DateTime.Now;
            DateTime? arrivalDate = DateTime.Now.AddHours(2);
            decimal price = 500.0m;
            int numberOfAvailableSeats = 100;
            Guid planId = Guid.NewGuid();

            // Act
            var flightEvent = new FlightRegisteredEvent(
                id, airline, flightNumber, departureCity, arrivalCity,
                departureDate, arrivalDate, price, numberOfAvailableSeats, planId);

            // Assert
            Assert.Equal(id, flightEvent.Id);
            Assert.Equal(airline, flightEvent.Airline);
            Assert.Equal(flightNumber, flightEvent.FlightNumber);
            Assert.Equal(departureCity, flightEvent.DepartureCity);
            Assert.Equal(arrivalCity, flightEvent.ArrivalCity);
            Assert.Equal(departureDate, flightEvent.DepartureDate);
            Assert.Equal(arrivalDate, flightEvent.ArrivalDate);
            Assert.Equal(price, flightEvent.Price);
            Assert.Equal(numberOfAvailableSeats, flightEvent.NumberOfAvailableSeats);
        }

        [Fact]
        public void Constructor_WithNullArrivalDate_SetsArrivalDateToNull()
        {
            // Arrange
            DateTime? arrivalDate = null;

            // Act
            var flightEvent = new FlightRegisteredEvent(
                Guid.NewGuid(), "Airline", "Flight123", "DepartureCity", "ArrivalCity",
                DateTime.Now, arrivalDate, 500.0m, 100, Guid.NewGuid());

            // Assert
            Assert.Null(flightEvent.ArrivalDate);
        }
    }
}
