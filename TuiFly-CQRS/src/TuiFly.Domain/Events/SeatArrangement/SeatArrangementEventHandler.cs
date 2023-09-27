using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TuiFly.Domain.Events.Reservation
{
    public class SeatArrangementEventHandler :
        INotificationHandler<SeatArrangementRegisteredEvent>
    {
        public Task Handle(SeatArrangementRegisteredEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}