using Microsoft.EntityFrameworkCore;
using Moq;
using NetDevPack.Data;
using NetDevPack.Mediator;
using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Commands.Customer.Handler;
using TuiFly.Domain.Interfaces;
using TuiFly.Domain.Models.Entities;
using TuiFly.Infra.Data.Context;
using TuiFly.Infra.Data.Repository;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Commands.Customer.Handler
{
    public class CustomerCommandHandlerTests
    {

        private readonly TuiFlyContext _context;
        private readonly ICustomerRepository _repository;
        private readonly Mock<ICustomerRepository> _repositoryMock;

        public CustomerCommandHandlerTests()
        {
            _repositoryMock = new Mock<ICustomerRepository>();
            var options = new DbContextOptionsBuilder<TuiFlyContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            // Create a mock of IMediatorHandler using Moq
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            _context = new TuiFlyContext(options, mediatorHandlerMock.Object);
            // Create a mock for IUnitOfWork
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            // Configure the _flightRepository mock to return the unitOfWorkMock
            _repositoryMock.Setup(x => x.UnitOfWork).Returns(unitOfWorkMock.Object);
            _repository = new CustomerRepository(_context);
        }

        [Fact]
        public async Task Handle_WithValidCommand_ShouldAddCustomerAndReturnSuccess()
        {
            // Arrange
            var handler = new CustomerCommandHandler(_repository);

            var command = new RegisterNewCustomerCommand("John", "Doe", "johndoe@example.com");
            var cancellationToken = new CancellationToken();

            // Act
            var result = await handler.Handle(command, cancellationToken);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task Handle_WithInvalidCommand_ShouldNotAddCustomerAndReturnValidationResult()
        {
            // Arrange
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var handler = new CustomerCommandHandler(customerRepositoryMock.Object);

            var invalidCommand = new RegisterNewCustomerCommand("", "Doe", "johndoe@example.com");
            var cancellationToken = new CancellationToken();

            // Act
            var result = await handler.Handle(invalidCommand, cancellationToken);

            // Assert
            customerRepositoryMock.Verify(repo => repo.Add(It.IsAny<CustomerEntity>()), Times.Never);
            Assert.False(result.IsValid);
        }
    }
}
