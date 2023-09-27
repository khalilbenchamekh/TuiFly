using TuiFly.Domain.Commands.Flight.Commands;
using TuiFly.Domain.Commands.Flight.Validation;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Commands.Flight.Validations
{
    public class FlightCommandValidationTests
    {
        [Fact]
        public void IsValid_WithValidFlightCommand_ShouldReturnTrue()
        {
            // Arrange
            var validFlightCommand = new FlightCommand
            {
                FlightId = Guid.NewGuid(),
                NumberOfAvailableSeats = 100
            };

            var flightCommandValidation = new FlightCommandValidation()
            {
                FlightId = validFlightCommand.FlightId,
                NumberOfAvailableSeats = validFlightCommand.NumberOfAvailableSeats
            };

            // Act
            var isValid = flightCommandValidation.IsValid();

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void IsValid_WithInvalidFlightCommand_ShouldReturnFalse()
        {
            // Arrange
            var invalidFlightCommand = new FlightCommand
            {
                FlightId = Guid.Empty, // Invalid FlightId
                NumberOfAvailableSeats = -10 // Invalid negative NumberOfAvailableSeats
            };

            var flightCommandValidation = new FlightCommandValidation()
            {
                FlightId = invalidFlightCommand.FlightId,
                NumberOfAvailableSeats = invalidFlightCommand.NumberOfAvailableSeats
            };

            // Act
            var isValid = flightCommandValidation.IsValid();

            // Assert
            Assert.False(isValid);
        }
    }

}
