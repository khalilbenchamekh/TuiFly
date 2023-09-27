
using TuiFly.Domain.Events.Flight;
using Xunit;


namespace TuiFLy.Domain.UnitTest.Events.Flight
{
    public class FlightEventHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidEvent_DoesNotThrowException()
        {
            // Arrange
            var handler = new FlightEventHandler();

            var flightEvent = new FlightRegisteredEvent(
                Guid.NewGuid(), "Airline", "Flight123", "DepartureCity", "ArrivalCity",
                DateTime.Now, DateTime.Now.AddHours(2), 500.0m, 100, Guid.NewGuid());

            // Act and Assert
            try
            {
                await handler.Handle(flightEvent, CancellationToken.None);
            }
            catch (Exception ex)
            {
                // If an exception is thrown, this assertion will fail.
                Assert.True(false, $"An exception was thrown: {ex.Message}");
            }
        }
    }
}
