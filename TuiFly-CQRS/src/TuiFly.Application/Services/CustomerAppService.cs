using System;
using System.Threading.Tasks;
using AutoMapper;
using TuiFly.Application.Interfaces;
using TuiFly.Domain.Interfaces;
using FluentValidation.Results;
using NetDevPack.Mediator;
using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Models.ViewModels;

namespace TuiFly.Application.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediatorHandler _mediator;
        private readonly IMapper _mapper;

        public CustomerAppService(IMapper mapper,
                                  ICustomerRepository customerRepository,
                                  IMediatorHandler mediator)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _mediator = mediator;
        }

        public async Task<CustomerViewModel> GetById(Guid id)
        {
            return _mapper.Map<CustomerViewModel>(await _customerRepository.GetById(id));
        }

        public async Task<ValidationResult> Register(CustomerViewModel customerViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewCustomerCommand>(customerViewModel);
            return await _mediator.SendCommand(registerCommand);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
