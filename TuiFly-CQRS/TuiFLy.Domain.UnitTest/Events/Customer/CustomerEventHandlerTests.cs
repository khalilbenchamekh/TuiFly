using TuiFly.Domain.Events.Customer;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Events.Customer
{
    public class CustomerEventHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldNotThrowException()
        {
            // Arrange
            var handler = new CustomerEventHandler();

            var customerRegisteredEvent = new CustomerRegisteredEvent(
                Guid.NewGuid(),
                "John",
                "Doe",
                "johndoe@example.com",
                "A1",
                Guid.NewGuid()
            );

            // Act & Assert
            try
            {
                await handler.Handle(customerRegisteredEvent, CancellationToken.None);
                // If no exception is thrown, the test passes.
            }
            catch (Exception ex)
            {
                // If an exception is caught, fail the test.
                Assert.True(false, $"Expected no exception, but got {ex.GetType()}: {ex.Message}");
            }
        }
    }
}
