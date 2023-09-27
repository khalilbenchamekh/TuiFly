using System;
using System.Threading.Tasks;
using FluentValidation.Results;
using TuiFly.Domain.Models.ViewModels;

namespace TuiFly.Application.Interfaces
{
    public interface ICustomerAppService : IDisposable
    {
        Task<CustomerViewModel> GetById(Guid id);
        Task<ValidationResult> Register(CustomerViewModel customerViewModel);
    }
}
