using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Commands.Validations.Customer;

namespace TuiFly.Domain.Commands.Customer.Validations.Customer
{
    public class RegisterNewCustomerCommandValidation : CustomerValidation<RegisterNewCustomerCommand>
    {
        public RegisterNewCustomerCommandValidation()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateEmail();
        }
    }
    public class CreateMultipleCustomersCommandValidator : CustomersValidation<CreateMultipleCustomersCommand>
    {
        public CreateMultipleCustomersCommandValidator()
        {
            ValidateCustomers();
        }
    }
}