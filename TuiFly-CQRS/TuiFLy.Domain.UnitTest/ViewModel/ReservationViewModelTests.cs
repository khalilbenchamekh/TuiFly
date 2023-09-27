using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TuiFly.Domain.Models.ViewModels;
using Xunit;

namespace TuiFLy.Domain.UnitTest.ViewModel
{
    public class ReservationViewModelTests
    {
        [Fact]
        public void FlightId_GetSet()
        {
            // Arrange
            var reservationViewModel = new ReservationViewModel();
            var flightId = Guid.NewGuid();

            // Act
            reservationViewModel.FlightId = flightId;

            // Assert
            Assert.Equal(flightId, reservationViewModel.FlightId);
        }

        [Fact]
        public void Passengers_GetSet()
        {
            // Arrange
            var reservationViewModel = new ReservationViewModel();
            var passengers = new List<PassengerViewModel>
        {
            new PassengerViewModel { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
            new PassengerViewModel { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
        };

            // Act
            reservationViewModel.Passengers = passengers;

            // Assert
            Assert.Equal(passengers, reservationViewModel.Passengers);
        }

        [Fact]
        public void FlightId_RequiredValidation()
        {
            // Arrange
            var reservationViewModel = new ReservationViewModel();
            var propertyInfo = typeof(ReservationViewModel).GetProperty("FlightId");

            // Act
            var requiredAttribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.Equal("The FlightId is required.", requiredAttribute.ErrorMessage);
        }

        [Fact]
        public void DisplayName_Attributes()
        {
            // Arrange
            var reservationViewModel = new ReservationViewModel();
            var flightIdProperty = typeof(ReservationViewModel).GetProperty("FlightId");

            // Act
            var flightIdDisplayName = flightIdProperty.GetCustomAttribute<DisplayNameAttribute>();

            // Assert
            Assert.NotNull(flightIdDisplayName);
            Assert.Equal("FlightId", flightIdDisplayName.DisplayName);
        }
    }
}
