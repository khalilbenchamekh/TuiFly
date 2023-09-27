using FluentValidation;
using System;
using TuiFly.Domain.Commands.Flight.Commands;

namespace TuiFly.Domain.Commands.Flight.Validation
{
    public abstract class FlightCommandValidator<T> : AbstractValidator<T> where T : FlightCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.FlightId)
                .NotEqual(Guid.Empty);
        }
        protected void NumberOfAvailableSeats()
        {
            RuleFor(x => x.NumberOfAvailableSeats).GreaterThanOrEqualTo(0).WithMessage("Number of available seats must be non-negative.");
        }
    }
    public class FlightMainCommandValidation : FlightCommandValidator<FlightCommand>
    {
        public FlightMainCommandValidation()
        {
            ValidateId();
            NumberOfAvailableSeats();
        }
    }

    public class FlightCommandValidation : FlightCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new FlightMainCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
