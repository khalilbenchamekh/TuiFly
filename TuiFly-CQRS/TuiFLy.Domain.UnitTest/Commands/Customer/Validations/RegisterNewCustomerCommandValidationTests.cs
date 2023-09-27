using FluentValidation.TestHelper;
using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Commands.Customer.Validations.Customer;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Commands.Customer.Validations
{
    public class RegisterNewCustomerCommandValidationTests
    {
        [Fact]
        public void Validate_WhenFirstNameIsNullOrEmpty_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new RegisterNewCustomerCommandValidation();
            var command = new RegisterNewCustomerCommand("Doe", "", "johndoe@example.com");

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage("Please ensure you have entered the Name");
        }

        [Fact]
        public void Validate_WhenFirstNameIsTooShort_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new RegisterNewCustomerCommandValidation();
            var command = new RegisterNewCustomerCommand("Doe", "J", "johndoe@example.com");

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage("The FirstName must have between 2 and 150 characters");
        }

        [Fact]
        public void Validate_WhenLastNameIsNullOrEmpty_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new RegisterNewCustomerCommandValidation();
            var command = new RegisterNewCustomerCommand("", "John", "johndoe@example.com");

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.LastName)
                .WithErrorMessage("Please ensure you have entered the Name");
        }

        [Fact]
        public void Validate_WhenLastNameIsTooShort_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new RegisterNewCustomerCommandValidation();
            var command = new RegisterNewCustomerCommand("D", "John", "johndoe@example.com");

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.LastName)
                .WithErrorMessage("The LastName must have between 2 and 150 characters");
        }

        [Fact]
        public void Validate_WhenEmailIsInvalid_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new RegisterNewCustomerCommandValidation();
            var command = new RegisterNewCustomerCommand("John", "Doe", "invalidemail");

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("'Email' is not a valid email address.");
        }

        [Fact]
        public void Validate_WhenAllPropertiesAreValid_ShouldNotHaveValidationError()
        {
            // Arrange
            var validator = new RegisterNewCustomerCommandValidation();
            var command = new RegisterNewCustomerCommand("John", "Doe", "johndoe@example.com");

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
            result.ShouldNotHaveValidationErrorFor(x => x.LastName);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }
    }
}
