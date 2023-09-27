using AutoMapper;
using FluentValidation.Results;
using Moq;
using NetDevPack.Mediator;
using TuiFly.Application.Services;
using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Interfaces;
using TuiFly.Domain.Models.Entities;
using TuiFly.Domain.Models.ViewModels;
using Xunit;

namespace TuiFly.Application.UnitTest.Services
{

    public class CustomerAppServiceTests
    {
        [Fact]
        public async Task GetById_Returns_CustomerViewModel()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            var customerViewModel = new CustomerViewModel(); // Create a sample CustomerViewModel

            // Setup the mock repository to return a customer when GetById is called
            customerRepositoryMock.Setup(repo => repo.GetById(customerId))
                .ReturnsAsync(new CustomerEntity()); // Assuming CustomerEntity is your domain entity

            // Setup the mapper to return the expected CustomerViewModel
            mapperMock.Setup(mapper => mapper.Map<CustomerViewModel>(It.IsAny<CustomerEntity>()))
                .Returns(customerViewModel);

            var customerAppService = new CustomerAppService(mapperMock.Object, customerRepositoryMock.Object, mediatorHandlerMock.Object);

            // Act
            var result = await customerAppService.GetById(customerId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Register_Returns_ValidationResult()
        {
            // Arrange
            var customerViewModel = new CustomerViewModel
            {
                LastName = "Doe",
                FirstName = "John",
                Email = "johndoe@example.com"
            };

            var registerCommand = new RegisterNewCustomerCommand(customerViewModel.LastName, customerViewModel.FirstName, customerViewModel.Email);
            var validationResult = new ValidationResult(); // Create a sample validation result

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var mapperMock = new Mock<IMapper>();

            // Setup the mapper to map the ViewModel to the command
            mapperMock.Setup(mapper => mapper.Map<RegisterNewCustomerCommand>(customerViewModel))
                .Returns(registerCommand);

            // Setup the mediator to return the expected validation result
            mediatorHandlerMock.Setup(handler => handler.SendCommand(registerCommand))
                .ReturnsAsync(validationResult);

            var customerAppService = new CustomerAppService(mapperMock.Object, customerRepositoryMock.Object, mediatorHandlerMock.Object);

            // Act
            var result = await customerAppService.Register(customerViewModel);

            // Assert
            Assert.NotNull(result);
        }
    }
}
