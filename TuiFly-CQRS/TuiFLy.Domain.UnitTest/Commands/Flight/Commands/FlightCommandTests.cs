using TuiFly.Domain.Commands.Flight.Commands;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Commands.Flight.Commands
{
    public class FlightCommandTests
    {
        [Fact]
        public void FlightCommand_Initialization_ShouldSetProperties()
        {
            // Arrange
            Guid flightId = Guid.NewGuid();
            int numberOfAvailableSeats = 100;

            // Act
            var flightCommand = new FlightCommand {
            FlightId = flightId, NumberOfAvailableSeats = numberOfAvailableSeats
            };

            // Assert
            Assert.Equal(flightId, flightCommand.FlightId);
            Assert.Equal(numberOfAvailableSeats, flightCommand.NumberOfAvailableSeats);
        }
    }

}
