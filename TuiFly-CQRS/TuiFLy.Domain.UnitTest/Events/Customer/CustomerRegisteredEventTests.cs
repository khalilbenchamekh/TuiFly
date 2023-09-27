using TuiFly.Domain.Events.Customer;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Events.Customer
{
    public class CustomerRegisteredEventTests
    {
        [Fact]
        public void Constructor_SetsProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "John";
            var lastName = "Doe";
            var email = "johndoe@example.com";
            var seatNumber = "A1";
            var reservationId = Guid.NewGuid();

            // Act
            var customerRegisteredEvent = new CustomerRegisteredEvent(id, firstName, lastName, email, seatNumber, reservationId);

            // Assert
            Assert.Equal(id, customerRegisteredEvent.Id);
            Assert.Equal(firstName, customerRegisteredEvent.FirstName);
            Assert.Equal(lastName, customerRegisteredEvent.LastName);
            Assert.Equal(email, customerRegisteredEvent.Email);
            Assert.Equal(seatNumber, customerRegisteredEvent.SeatNumber);
            Assert.Equal(reservationId, customerRegisteredEvent.ReservationId);
            Assert.Equal(id, customerRegisteredEvent.AggregateId);
        }
    }
}
