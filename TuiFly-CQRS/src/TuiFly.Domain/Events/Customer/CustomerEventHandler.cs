using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TuiFly.Domain.Events.Customer
{
    public class CustomerEventHandler :
        INotificationHandler<CustomerRegisteredEvent>
    {
        public Task Handle(CustomerRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }
    }
}