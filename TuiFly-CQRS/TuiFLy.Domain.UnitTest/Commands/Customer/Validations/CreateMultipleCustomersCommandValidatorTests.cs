using FluentValidation.TestHelper;
using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Commands.Customer.Validations.Customer;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Commands.Customer.Validations
{

    public class CreateMultipleCustomersCommandValidatorTests
    {
        [Fact]
        public void Validate_WhenCustomersListIsNull_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new CreateMultipleCustomersCommandValidator();
            var command = new CreateMultipleCustomersCommand { Customers = null };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Customers)
                .WithErrorMessage("Customers list cannot be empty.");
        }

        [Fact]
        public void Validate_WhenCustomersListIsEmpty_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new CreateMultipleCustomersCommandValidator();
            var command = new CreateMultipleCustomersCommand { Customers = new List<RegisterNewCustomerCommand>() };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Customers)
                .WithErrorMessage("Customers list cannot be empty.");
        }

        [Fact]
        public void Validate_WhenCustomersListIsValid_ShouldNotHaveValidationError()
        {
            // Arrange
            var validator = new CreateMultipleCustomersCommandValidator();
            var command = new CreateMultipleCustomersCommand
            {
                Customers = new List<RegisterNewCustomerCommand>
            {
                new RegisterNewCustomerCommand("Doe", "John", "johndoe@example.com"),
                new RegisterNewCustomerCommand("Smith", "Jane", "janesmith@example.com")
            }
            };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Customers);
        }
    }
}
