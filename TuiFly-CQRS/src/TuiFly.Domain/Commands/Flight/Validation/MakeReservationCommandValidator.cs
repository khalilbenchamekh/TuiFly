using FluentValidation;
using System;
using TuiFly.Domain.Commands.Customer.Validations.Customer;
using TuiFly.Domain.Commands.Flight.Commands;

namespace TuiFly.Domain.Commands.Flight.Validation
{
   
    public abstract class MakeReservationCommandValidator<T> : AbstractValidator<T> where T : MakeReservationCommand
    {
        protected void ValidateCustomers()
        {
            RuleFor(c => c.Passengers)
                .NotNull()
                .NotEmpty()
                .WithMessage("Customers list cannot be empty.");

            RuleForEach(c => c.Passengers).SetValidator(new RegisterNewCustomerCommandValidation());
        }
        protected void ValidateFlight()
        {
            RuleFor(c => c.FlightId)
               .NotEqual(Guid.Empty);
        }
    }
    public class MakeReservationMainCommandValidation : MakeReservationCommandValidator<MakeReservationCommand>
    {
        public MakeReservationMainCommandValidation()
        {
            ValidateCustomers();
            ValidateFlight();
        }
    }

    public class MakeReservationCommandValidation : MakeReservationCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new MakeReservationMainCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
