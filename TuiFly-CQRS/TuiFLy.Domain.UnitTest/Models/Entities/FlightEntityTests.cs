using TuiFly.Domain.Models.Entities;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Models.Entities
{

    public class FlightEntityTests
    {
        [Fact]
        public void Constructor_WithValidParameters_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var airline = "Emirates";
            var flightNumber = "EK123";
            var departureCity = "New York";
            var arrivalCity = "Dubai";
            var departureDate = DateTime.UtcNow;
            var arrivalDate = DateTime.UtcNow.AddHours(15);
            var price = 1200.50m;
            var numberOfAvailableSeats = 250;
            var planId = Guid.NewGuid();

            // Act
            var flight = new FlightEntity(id, airline, flightNumber, departureCity, arrivalCity, departureDate, arrivalDate, price, numberOfAvailableSeats, planId);

            // Assert
            Assert.Equal(id, flight.Id);
            Assert.Equal(airline, flight.Airline);
            Assert.Equal(flightNumber, flight.FlightNumber);
            Assert.Equal(departureCity, flight.DepartureCity);
            Assert.Equal(arrivalCity, flight.ArrivalCity);
            Assert.Equal(departureDate, flight.DepartureDate);
            Assert.Equal(arrivalDate, flight.ArrivalDate);
            Assert.Equal(price, flight.Price);
            Assert.Equal(numberOfAvailableSeats, flight.NumberOfAvailableSeats);
            Assert.Equal(planId, flight.PlanId);
        }

        [Fact]
        public void Constructor_WithDefaultValues_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.Empty;
            var airline = "";
            var flightNumber = "";
            var departureCity = "";
            var arrivalCity = "";
            var departureDate = DateTime.MinValue;
            var arrivalDate = (DateTime?)null;
            var price = 0m;
            var numberOfAvailableSeats = 0;
            var planId = Guid.Empty;

            // Act
            var flight = new FlightEntity(id, airline, flightNumber, departureCity, arrivalCity, departureDate, arrivalDate, price, numberOfAvailableSeats, planId);

            // Assert
            Assert.Equal(id, flight.Id);
            Assert.Equal(airline, flight.Airline);
            Assert.Equal(flightNumber, flight.FlightNumber);
            Assert.Equal(departureCity, flight.DepartureCity);
            Assert.Equal(arrivalCity, flight.ArrivalCity);
            Assert.Equal(departureDate, flight.DepartureDate);
            Assert.Equal(arrivalDate, flight.ArrivalDate);
            Assert.Equal(price, flight.Price);
            Assert.Equal(numberOfAvailableSeats, flight.NumberOfAvailableSeats);
            Assert.Equal(planId, flight.PlanId);
        }
    }
}
