using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Commands.Flight.Commands;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Commands.Flight.Commands
{
    public class MakeReservationCommandTests
    {
        [Fact]
        public void MakeReservationCommand_Initialization_ShouldSetProperties()
        {
            // Arrange
            Guid flightId = Guid.NewGuid();
            var passengers = new List<RegisterNewCustomerCommand>
        {
            new RegisterNewCustomerCommand("John", "Doe", "john@example.com"),
            new RegisterNewCustomerCommand("Jane", "Smith", "jane@example.com")
        };

            // Act
            var makeReservationCommand = new MakeReservationCommand
            {
                FlightId = flightId,
                Passengers = passengers
            };

            // Assert
            Assert.Equal(flightId, makeReservationCommand.FlightId);
            Assert.Equal(passengers, makeReservationCommand.Passengers);
        }

        [Fact]
        public void MakeReservationCommand_ShouldHaveDefaultConstructor()
        {
            // Arrange
            // Act
            var makeReservationCommand = new MakeReservationCommand();

            // Assert
            Assert.Null(makeReservationCommand.Passengers);
        }

        [Fact]
        public void MakeReservationCommand_ShouldAddPassenger()
        {
            // Arrange
            var makeReservationCommand = new MakeReservationCommand();
            var passengerCommand = new RegisterNewCustomerCommand("John", "Doe", "john@example.com");

            // Act
            makeReservationCommand.Passengers = new List<RegisterNewCustomerCommand> { passengerCommand};

            // Assert
            Assert.Single(makeReservationCommand.Passengers);
            Assert.Equal(passengerCommand, makeReservationCommand.Passengers[0]);
        }
    }

}
