using FluentValidation;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Commands.Validations.Customer;
using TuiFLy.Domain.UnitTest.Commands.Customer.Commands;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Commands.Customer.Validations
{

    public class CustomerValidationTests
    {
        [Fact]
        public void ValidateFirstName_Should_Have_Error_When_FirstName_Is_Empty()
        {
            // Arrange
            var validator = new TestCustomerValidation();
            var command = new TestCustomerCommand("John", string.Empty, "johndoe@example.com");
            

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.FirstName)
                .WithErrorMessage("First Name is required");
        }

        [Fact]
        public void ValidateFirstName_Should_Have_Error_When_FirstName_Length_Is_Too_Short()
        {
            // Arrange
            var validator = new TestCustomerValidation();
            var command = new TestCustomerCommand("John", "A", "johndoe@example.com");

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.FirstName)
                .WithErrorMessage("First Name must be between 2 and 150 characters");
        }

        [Fact]
        public void ValidateFirstName_Should_Have_Error_When_FirstName_Length_Is_Too_Long()
        {
            // Arrange
            var validator = new TestCustomerValidation();
            var command = new TestCustomerCommand("John", new string('A', 151), "johndoe@example.com");

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.FirstName)
                .WithErrorMessage("First Name must be between 2 and 150 characters");
        }
    }
    public class TestCustomerValidation : CustomerValidation<TestCustomerCommand>
    {
        public TestCustomerValidation()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("First Name is required")
                .Length(2, 150).WithMessage("First Name must be between 2 and 150 characters");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Last Name is required")
                .Length(2, 150).WithMessage("Last Name must be between 2 and 150 characters");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("Id must not be empty");

            RuleFor(c => c.SeatNumber)
                .NotEmpty().WithMessage("Seat Number is required")
                .MaximumLength(10).WithMessage("Seat Number cannot exceed 10 characters");

            RuleFor(c => c.ReservationId)
                .NotEqual(Guid.Empty).WithMessage("ReservationId must not be empty");
        }
    }
    public class TestCustomerCommand : CustomerCommand
    {
        public TestCustomerCommand(string lastName, string firstName, string email)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
        }
    }
}
