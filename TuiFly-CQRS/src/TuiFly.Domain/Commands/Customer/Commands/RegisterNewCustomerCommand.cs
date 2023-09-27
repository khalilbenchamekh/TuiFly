using NetDevPack.Messaging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TuiFly.Domain.Commands.Customer.Validations.Customer;

namespace TuiFly.Domain.Commands.Customer.Commands
{
    public class RegisterNewCustomerCommand : CustomerCommand
    {
        public RegisterNewCustomerCommand(string lastName, string firstName, string email)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CreateMultipleCustomersCommand : Command
    {
        public List<RegisterNewCustomerCommand> Customers { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new CreateMultipleCustomersCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}