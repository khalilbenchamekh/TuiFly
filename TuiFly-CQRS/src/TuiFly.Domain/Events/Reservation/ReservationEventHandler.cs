using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TuiFly.Domain.Events.Reservation
{
    public class ReservationEventHandler :
        INotificationHandler<ReservationRegisteredEvent>
    {
        public Task Handle(ReservationRegisteredEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}