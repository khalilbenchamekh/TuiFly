using TuiFly.Domain.Models.Entities;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Models.Entities
{
    public class CustomerEntityTests
    {
        [Fact]
        public void Constructor_WithValidParameters_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "John";
            var lastName = "Doe";
            var email = "john.doe@example.com";
            var seatNumber = "A1";
            var reservationId = Guid.NewGuid();

            // Act
            var customer = new CustomerEntity(id, firstName, lastName, email, seatNumber, reservationId);

            // Assert
            Assert.Equal(id, customer.Id);
            Assert.Equal(firstName, customer.FirstName);
            Assert.Equal(lastName, customer.LastName);
            Assert.Equal(email, customer.Email);
            Assert.Equal(seatNumber, customer.SeatNumber);
            Assert.Equal(reservationId, customer.ReservationId);
        }

        [Fact]
        public void Constructor_WithEmptyValues_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.Empty;
            var firstName = "";
            var lastName = "";
            var email = "";
            var seatNumber = "";
            var reservationId = Guid.Empty;

            // Act
            var customer = new CustomerEntity(id, firstName, lastName, email, seatNumber, reservationId);

            // Assert
            Assert.Equal(id, customer.Id);
            Assert.Equal(firstName, customer.FirstName);
            Assert.Equal(lastName, customer.LastName);
            Assert.Equal(email, customer.Email);
            Assert.Equal(seatNumber, customer.SeatNumber);
            Assert.Equal(reservationId, customer.ReservationId);
        }

        [Fact]
        public void Constructor_WithNullValues_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            string firstName = null;
            string lastName = null;
            string email = null;
            string seatNumber = null;
            var reservationId = Guid.NewGuid();

            // Act
            var customer = new CustomerEntity(id, firstName, lastName, email, seatNumber, reservationId);

            // Assert
            Assert.Equal(id, customer.Id);
            Assert.Null(customer.FirstName);
            Assert.Null(customer.LastName);
            Assert.Null(customer.Email);
            Assert.Null(customer.SeatNumber);
            Assert.Equal(reservationId, customer.ReservationId);
        }
    }
}
