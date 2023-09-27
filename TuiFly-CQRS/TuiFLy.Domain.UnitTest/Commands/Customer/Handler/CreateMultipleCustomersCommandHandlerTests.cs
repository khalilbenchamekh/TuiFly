using FluentValidation.Results;
using MediatR;
using Moq;
using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Commands.Customer.Handler;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Commands.Customer.Handler
{
    public class CreateMultipleCustomersCommandHandlerTests
    {
        private readonly Mock<IMediator> _mediatorMock;

        public CreateMultipleCustomersCommandHandlerTests()
        {
            _mediatorMock = new Mock<IMediator>();
        }
        [Fact]
        public async Task Handle_ValidInputWithMultipleCustomers_ShouldReturnValidResult()
        {
            // Arrange
            var handler = new CreateMultipleCustomersCommandHandler(_mediatorMock.Object);

            var customers = new List<RegisterNewCustomerCommand>
            {
                new RegisterNewCustomerCommand("John", "Doe", "johndoe@example.com"),
                new RegisterNewCustomerCommand("Jane", "Smith", "janesmith@example.com")
            };

            var command = new CreateMultipleCustomersCommand { Customers = customers };
            var cancellationToken = new CancellationToken();

            // Configure mediator to return valid results for individual commands
            _mediatorMock.Setup(m => m.Send(It.IsAny<RegisterNewCustomerCommand>(), cancellationToken))
                        .ReturnsAsync(new ValidationResult());

            // Act
            var result = await handler.Handle(command, cancellationToken);

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);

            // Verify that mediator is called for each customer
            _mediatorMock.Verify(m => m.Send(It.IsAny<RegisterNewCustomerCommand>(), cancellationToken), Times.Exactly(customers.Count));
        }
        [Fact]
        public async Task Handle_EmptyCustomerList_ShouldReturnValidationError()
        {
            // Arrange
            var handler = new CreateMultipleCustomersCommandHandler(_mediatorMock.Object);

            var command = new CreateMultipleCustomersCommand { Customers = new List<RegisterNewCustomerCommand>() };
            var cancellationToken = new CancellationToken();

            // Act
            var result = await handler.Handle(command, cancellationToken);

            // Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Equal("CustomerCommands", result.Errors.First().PropertyName);
            Assert.Equal("The list of customer commands is empty.", result.Errors.First().ErrorMessage);

            // Verify that mediator is not called
            _mediatorMock.Verify(m => m.Send(It.IsAny<RegisterNewCustomerCommand>(), cancellationToken), Times.Never);
        }

        [Fact]
        public async Task Handle_IndividualCustomerValidationFailure_ShouldReturnCombinedValidationResult()
        {
            // Arrange
            var handler = new CreateMultipleCustomersCommandHandler(_mediatorMock.Object);

            var customers = new List<RegisterNewCustomerCommand>
    {
        new RegisterNewCustomerCommand("John", "Doe", "invalid-email"), // Invalid email
        new RegisterNewCustomerCommand("Jane", "", "janesmith@example.com"), // Empty first name
    };

            var command = new CreateMultipleCustomersCommand { Customers = customers };
            var cancellationToken = new CancellationToken();

            // Configure mediator to return validation failures for individual commands
            var validationFailures = new List<ValidationFailure>
    {
        new ValidationFailure("Email", "Invalid email format."),
        new ValidationFailure("FirstName", "Please ensure you have entered the Name")
    };

            _mediatorMock.SetupSequence(m => m.Send(It.IsAny<RegisterNewCustomerCommand>(), cancellationToken))
                        .ReturnsAsync(new ValidationResult(validationFailures))
                        .ReturnsAsync(new ValidationResult(validationFailures));

            // Act
            var result = await handler.Handle(command, cancellationToken);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(4, result.Errors.Count);
            Assert.Contains(result.Errors, error => error.PropertyName == "Email");
            Assert.Contains(result.Errors, error => error.PropertyName == "FirstName");

            // Verify that mediator is called for each customer
            _mediatorMock.Verify(m => m.Send(It.IsAny<RegisterNewCustomerCommand>(), cancellationToken), Times.Exactly(customers.Count));
        }

    }
}
