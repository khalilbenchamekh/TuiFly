using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TuiFly.Domain.Events.Flight
{
    public class FlightEventHandler :
        INotificationHandler<FlightRegisteredEvent>
    {
        public Task Handle(FlightRegisteredEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}