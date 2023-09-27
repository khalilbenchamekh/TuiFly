using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuiFly.Domain.Models.Response;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Models.Response
{
    public class FlightResponseTests
    {
        [Fact]
        public void Constructor_WithValidParameters_SetsProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var airline = "Airline1";
            var flightNumber = "FL123";
            var departureCity = "City1";
            var arrivalCity = "City2";
            var departureDate = DateTime.Parse("2023-09-20");
            var arrivalDate = DateTime.Parse("2023-09-21");
            var price = 100.50m;
            var numberOfAvailableSeats = 150;
            var planId = Guid.NewGuid();
            var planResponse = new PlanResponse();
            var reservations = new List<ReservationResponse> { new ReservationResponse() };

            // Act
            var flightResponse = new FlightResponse
            {
                Id = id,
                Airline = airline,
                FlightNumber = flightNumber,
                DepartureCity = departureCity,
                ArrivalCity = arrivalCity,
                DepartureDate = departureDate,
                ArrivalDate = arrivalDate,
                Price = price,
                NumberOfAvailableSeats = numberOfAvailableSeats,
                PlanId = planId,
                Plan = planResponse,
                Reservations = reservations
            };

            // Assert
            // Assert
            Assert.Equal(id, flightResponse.Id);
            Assert.Equal(airline, flightResponse.Airline);
            Assert.Equal(flightNumber, flightResponse.FlightNumber);
            Assert.Equal(departureCity, flightResponse.DepartureCity);
            Assert.Equal(arrivalCity, flightResponse.ArrivalCity);
            Assert.Equal(departureDate, flightResponse.DepartureDate);
            Assert.Equal(arrivalDate, flightResponse.ArrivalDate);
            Assert.Equal(price, flightResponse.Price);
            Assert.Equal(numberOfAvailableSeats, flightResponse.NumberOfAvailableSeats);
            Assert.Equal(planId, flightResponse.PlanId);
            Assert.Equal(planResponse, flightResponse.Plan);
            Assert.Equal(reservations, flightResponse.Reservations);
        }

        [Fact]
        public void Constructor_WithDefaultValues_SetsProperties()
        {
            // Arrange

            // Act
            var flightResponse = new FlightResponse();

            // Assert
            Assert.Equal(Guid.Empty, flightResponse.Id);
            Assert.Null(flightResponse.Airline);
            Assert.Null(flightResponse.FlightNumber);
            Assert.Null(flightResponse.DepartureCity);
            Assert.Null(flightResponse.ArrivalCity);
            Assert.Equal(default(DateTime), flightResponse.DepartureDate);
            Assert.Equal(default(DateTime), flightResponse.ArrivalDate);
            Assert.Equal(0m, flightResponse.Price);
            Assert.Equal(0, flightResponse.NumberOfAvailableSeats);
            Assert.Equal(Guid.Empty, flightResponse.PlanId);
            Assert.Null(flightResponse.Plan);
            Assert.Null(flightResponse.Reservations);
        }
    }
}
