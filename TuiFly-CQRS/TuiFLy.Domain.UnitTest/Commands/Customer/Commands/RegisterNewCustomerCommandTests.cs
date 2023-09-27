using TuiFly.Domain.Commands.Customer.Commands;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Commands.Customer.Commands
{
    public class RegisterNewCustomerCommandTests
    {
        [Fact]
        public void RegisterNewCustomerCommand_Should_Initialize_Properties_Correctly()
        {
            // Arrange
            var lastName = "Doe";
            var firstName = "John";
            var email = "johndoe@example.com";

            // Act
            var command = new RegisterNewCustomerCommand(lastName, firstName, email);

            // Assert
            Assert.Equal(lastName, command.LastName);
            Assert.Equal(firstName, command.FirstName);
            Assert.Equal(email, command.Email);
        }

        [Fact]
        public void RegisterNewCustomerCommand_Should_Return_Valid_When_Properties_Are_Set_Correctly()
        {
            // Arrange
            var lastName = "Doe";
            var firstName = "John";
            var email = "johndoe@example.com";

            // Act
            var command = new RegisterNewCustomerCommand(lastName, firstName, email);
            var isValid = command.IsValid();

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void RegisterNewCustomerCommand_Should_Return_Invalid_When_Properties_Are_Missing()
        {
            // Arrange
            var lastName = "Doe";
            var firstName = "John";

            // Act
            var command = new RegisterNewCustomerCommand(lastName, firstName, null);
            var isValid = command.IsValid();

            // Assert
            Assert.False(isValid);
        }

    }
    public class CreateMultipleCustomersCommandTests
    {
        [Fact]
        public void CreateMultipleCustomersCommand_Should_Initialize_Properties_Correctly()
        {
            // Arrange
            var customers = new List<RegisterNewCustomerCommand>
        {
            new RegisterNewCustomerCommand("Doe", "John", "johndoe@example.com"),
            new RegisterNewCustomerCommand("Smith", "Jane", "janesmith@example.com")
        };

            // Act
            var command = new CreateMultipleCustomersCommand { Customers = customers };

            // Assert
            Assert.Equal(customers, command.Customers);
        }

        [Fact]
        public void CreateMultipleCustomersCommand_Should_Return_Valid_When_Customers_Are_Valid()
        {
            // Arrange
            var customers = new List<RegisterNewCustomerCommand>
        {
            new RegisterNewCustomerCommand("Doe", "John", "johndoe@example.com"),
            new RegisterNewCustomerCommand("Smith", "Jane", "janesmith@example.com")
        };

            // Act
            var command = new CreateMultipleCustomersCommand { Customers = customers };
            var isValid = command.IsValid();

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void CreateMultipleCustomersCommand_Should_Return_Invalid_When_Any_Customer_Is_Invalid()
        {
            // Arrange
            var customers = new List<RegisterNewCustomerCommand>
        {
            new RegisterNewCustomerCommand("Doe", "John", "johndoe@example.com"),
            new RegisterNewCustomerCommand("Smith", null, "janesmith@example.com") // Invalid customer with missing first name
        };

            // Act
            var command = new CreateMultipleCustomersCommand { Customers = customers };
            var isValid = command.IsValid();

            // Assert
            Assert.False(isValid);
        }
    }
}
