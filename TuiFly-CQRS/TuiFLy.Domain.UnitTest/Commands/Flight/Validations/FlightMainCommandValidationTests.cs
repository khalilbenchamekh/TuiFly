using TuiFly.Domain.Commands.Flight.Commands;
using TuiFly.Domain.Commands.Flight.Validation;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Commands.Flight.Validations
{
    public class FlightMainCommandValidationTests
    {
        [Fact]
        public void FlightMainCommandValidation_WithValidFlightId_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var validator = new FlightMainCommandValidation();
            var validFlightCommand = new FlightCommand
            {
                FlightId = Guid.NewGuid(),
                NumberOfAvailableSeats = 100
            };

            // Act
            var validationResult = validator.Validate(validFlightCommand);

            // Assert
            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public void FlightMainCommandValidation_WithEmptyFlightId_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new FlightMainCommandValidation();
            var invalidFlightCommand = new FlightCommand
            {
                FlightId = Guid.Empty,
                NumberOfAvailableSeats = 100
            };

            // Act
            var validationResult = validator.Validate(invalidFlightCommand);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
            Assert.Contains(validationResult.Errors, error => error.PropertyName == "FlightId");
        }

        [Fact]
        public void FlightMainCommandValidation_WithNegativeAvailableSeats_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new FlightMainCommandValidation();
            var invalidFlightCommand = new FlightCommand
            {
                FlightId = Guid.NewGuid(),
                NumberOfAvailableSeats = -10
            };

            // Act
            var validationResult = validator.Validate(invalidFlightCommand);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
            Assert.Contains(validationResult.Errors, error => error.PropertyName == "NumberOfAvailableSeats");
        }
    }

}
