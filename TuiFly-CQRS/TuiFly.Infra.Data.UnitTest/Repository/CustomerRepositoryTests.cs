using Microsoft.EntityFrameworkCore;
using Moq;
using NetDevPack.Data;
using NetDevPack.Mediator;
using TuiFly.Domain.Interfaces;
using TuiFly.Domain.Models.Entities;
using TuiFly.Infra.Data.Context;
using TuiFly.Infra.Data.Repository;
using Xunit;

namespace TuiFly.Infra.Data.UnitTest.Repository
{
    public class CustomerRepositoryTests : IDisposable
    {
        private readonly TuiFlyContext _context;
        private readonly ICustomerRepository _repository;
        private readonly Mock<ICustomerRepository> _repositoryMock;

        public CustomerRepositoryTests()
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

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new CustomerEntity(customerId, "John", "Doe", "john@example.com", "A1", Guid.NewGuid());
            _context.Customers.Add(customer);
            _context.SaveChanges();

            // Act
            var result = await _repository.GetById(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.Id);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("john@example.com", result.Email);
        }

        [Fact]
        public void Add_ValidCustomer_CustomerAdded()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new CustomerEntity(customerId, "Jane", "Smith", "jane@example.com", "A1", Guid.NewGuid());

            // Act
            _repository.Add(customer);
            _context.SaveChanges();

            // Assert
            var addedCustomer = _context.Customers.Find(customerId);
            Assert.NotNull(addedCustomer);
            Assert.Equal("Jane", addedCustomer.FirstName);
            Assert.Equal("Smith", addedCustomer.LastName);
            Assert.Equal("jane@example.com", addedCustomer.Email);
        }

        [Fact]
        public void Update_ValidCustomer_CustomerUpdated()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new CustomerEntity(customerId, "John", "Doe", "johndoe@example.com", "A1", Guid.NewGuid());
            _context.Customers.Add(customer);
            _context.SaveChanges();
            // Modify customer data
            customer.FirstName = "UpdatedFirstName";
            customer.LastName = "UpdatedLastName";
            customer.Email = "updated@example.com";

            // Act
            _repository.Update(customer);
            _context.SaveChanges();

            // Assert
            var updatedCustomer = _context.Customers.Find(customerId);
            Assert.NotNull(updatedCustomer);
            Assert.Equal("UpdatedFirstName", updatedCustomer.FirstName);
            Assert.Equal("UpdatedLastName", updatedCustomer.LastName);
            Assert.Equal("updated@example.com", updatedCustomer.Email);
        }
    }

}
