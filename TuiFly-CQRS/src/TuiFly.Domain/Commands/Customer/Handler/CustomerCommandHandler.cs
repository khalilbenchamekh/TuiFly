using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Events.Customer;
using TuiFly.Domain.Interfaces;
using TuiFly.Domain.Models.Entities;

namespace TuiFly.Domain.Commands.Customer.Handler
{
    public class CustomerCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> Handle(RegisterNewCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            var customer = new CustomerEntity(Guid.NewGuid(), request.FirstName, request.LastName, request.Email, request.SeatNumber, request.ReservationId);

            customer.AddDomainEvent(new CustomerRegisteredEvent(customer.Id, customer.LastName, customer.FirstName, customer.Email, customer.SeatNumber, customer.ReservationId));

            _customerRepository.Add(customer);

            var res = await Commit(_customerRepository.UnitOfWork);
            return res;
        }

        public void Dispose()
        {
            _customerRepository.Dispose();
        }
    }
}