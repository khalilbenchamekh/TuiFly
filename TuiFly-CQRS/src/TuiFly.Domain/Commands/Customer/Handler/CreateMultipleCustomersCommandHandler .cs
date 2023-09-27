using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TuiFly.Domain.Commands.Customer.Commands;

namespace TuiFly.Domain.Commands.Customer.Handler
{
    public class CreateMultipleCustomersCommandHandler : CommandHandler,
        IRequestHandler<CreateMultipleCustomersCommand, ValidationResult>
    {
        private readonly IMediator _mediator;

        public CreateMultipleCustomersCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ValidationResult> Handle(CreateMultipleCustomersCommand message, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResult();

            if (message.Customers == null || !message.Customers.Any())
            {
                validationResult.Errors.Add(new ValidationFailure("CustomerCommands", "The list of customer commands is empty."));
            }
            else
            {
                foreach (var customerCommand in message.Customers)
                {
                    var individualValidationResult = await _mediator.Send(customerCommand);

                    if (!individualValidationResult.IsValid)
                    {
                        validationResult.Errors.AddRange(individualValidationResult.Errors);
                    }
                }
            }

            return validationResult;
        }
    }
}