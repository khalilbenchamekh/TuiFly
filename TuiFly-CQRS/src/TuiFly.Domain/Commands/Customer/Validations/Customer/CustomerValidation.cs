using FluentValidation;
using System;
using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Commands.Customer.Validations.Customer;

namespace TuiFly.Domain.Commands.Validations.Customer
{
    public abstract class CustomersValidation<T> : AbstractValidator<T> where T : CreateMultipleCustomersCommand
    {
        protected void ValidateCustomers()
        {
            RuleFor(c => c.Customers)
                .NotNull()
                .NotEmpty()
                .WithMessage("Customers list cannot be empty.");

            RuleForEach(c => c.Customers).SetValidator(new RegisterNewCustomerCommandValidation());
        }
    }
    public abstract class CustomerValidation<T> : AbstractValidator<T> where T : CustomerCommand
    {
        protected void ValidateFirstName()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 150).WithMessage("The FirstName must have between 2 and 150 characters");
        }

        protected void ValidateLastName()
        {
            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 150).WithMessage("The LastName must have between 2 and 150 characters");
        }

        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
        protected void ValidateSeatNumber()
        {
            RuleFor(x => x.SeatNumber).NotEmpty().MaximumLength(10);
        }
        protected void ValidateReservationId()
        {
            RuleFor(x => x.ReservationId).NotEqual(Guid.Empty); ;
        }
    }
}