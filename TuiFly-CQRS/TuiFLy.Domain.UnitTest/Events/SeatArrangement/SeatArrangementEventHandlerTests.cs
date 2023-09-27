using TuiFly.Domain.Events.Reservation;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Events.SeatArrangement
{
    public class SeatArrangementEventHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidEvent_ReturnsCompletedTask()
        {
            // Arrange
            var handler = new SeatArrangementEventHandler();
            var seatArrangementEvent = new SeatArrangementRegisteredEvent(
                Guid.NewGuid(),
                "A1",
                true,
                Guid.NewGuid());

            // Act and Assert
            try
            {
                await handler.Handle(seatArrangementEvent, CancellationToken.None);
            }
            catch (Exception ex)
            {
                // If an exception is thrown, this assertion will fail.
                Assert.True(false, $"An exception was thrown: {ex.Message}");
            }
        }
    }
}
