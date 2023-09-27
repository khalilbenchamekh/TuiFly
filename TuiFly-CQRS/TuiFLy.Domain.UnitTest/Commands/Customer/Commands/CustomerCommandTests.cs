using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuiFly.Domain.Commands.Customer.Commands;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Commands.Customer.Commands
{
    public class CustomerCommandTests
    {
        [Fact]
        public void CustomerCommand_Should_Set_Properties_Correctly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "John";
            var lastName = "Doe";
            var email = "johndoe@example.com";
            var seatNumber = "A1";
            var reservationId = Guid.NewGuid();

            // Act
            var customerCommand = new TestCustomerCommand
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                SeatNumber = seatNumber,
                ReservationId = reservationId
            };

            // Assert
            Assert.Equal(id, customerCommand.Id);
            Assert.Equal(firstName, customerCommand.FirstName);
            Assert.Equal(lastName, customerCommand.LastName);
            Assert.Equal(email, customerCommand.Email);
            Assert.Equal(seatNumber, customerCommand.SeatNumber);
            Assert.Equal(reservationId, customerCommand.ReservationId);
        }

        // Add more test methods to cover other behaviors if needed
    }

    // Create a test class that inherits from CustomerCommand for testing
    public class TestCustomerCommand : CustomerCommand
    {
    }
}
