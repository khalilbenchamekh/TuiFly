using TuiFly.Domain.Events.Reservation;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Events.Reservation
{
    public class ReservationEventHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidEvent_DoesNotThrowException()
        {
            // Arrange
            var handler = new ReservationEventHandler();

            var reservationEvent = new ReservationRegisteredEvent(
                Guid.NewGuid(), 2, "A1", DateTime.Now, Guid.NewGuid());

            // Act and Assert
            try
            {
                await handler.Handle(reservationEvent, CancellationToken.None);
            }
            catch (Exception ex)
            {
                // If an exception is thrown, this assertion will fail.
                Assert.True(false, $"An exception was thrown: {ex.Message}");
            }
        }
    }
}
