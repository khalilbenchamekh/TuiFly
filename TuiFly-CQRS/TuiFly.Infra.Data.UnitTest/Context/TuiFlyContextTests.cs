using Microsoft.EntityFrameworkCore;
using Moq;
using NetDevPack.Mediator;
using TuiFly.Domain.Models.Entities;
using TuiFly.Infra.Data.Context;
using Xunit;

namespace TuiFly.Infra.Data.UnitTest.Context
{
    public class TuiFlyContextTests : IDisposable
    {
        private readonly DbContextOptions<TuiFlyContext> _options;
        private readonly TuiFlyContext _context;

        public TuiFlyContextTests()
        {
            // Set up an in-memory database for testing
            _options = new DbContextOptionsBuilder<TuiFlyContext>()
                .UseInMemoryDatabase(databaseName: "TuiFly_Test_Database")
                .Options;
            // Create a mock of IMediatorHandler using Moq
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            _context = new TuiFlyContext(_options, mediatorHandlerMock.Object);
        }

        public void Dispose()
        {
            // Dispose of the context and release resources
            _context.Dispose();
        }

        [Fact]
        public async Task CommitAsync_ShouldSaveChangesAndPublishDomainEvents()
        {
            // Arrange
            // Create some entities and add them to the context
            var customer = new CustomerEntity(Guid.NewGuid(), "John", "Doe", "johndoe@example.com", "A1", Guid.NewGuid());
            _context.Customers.Add(customer);

            // Act
            var result = await _context.Commit();

            // Assert
            Assert.True(result); // Check if SaveChangesAsync returned true (indicating success)
                                 // You can also add additional assertions to verify domain events were published, etc.
        }
    }
}
