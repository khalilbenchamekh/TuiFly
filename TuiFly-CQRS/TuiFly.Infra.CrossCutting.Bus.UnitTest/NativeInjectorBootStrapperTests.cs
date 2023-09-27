using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using TuiFly.Application.Interfaces;
using Xunit;

namespace TuiFly.Infra.CrossCutting.IoC.UnitTest
{
    public class NativeInjectorBootStrapperTests
    {
        [Fact]
        public void RegisterServices_ShouldRegisterMediatorHandlerAsScoped()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            NativeInjectorBootStrapper.RegisterServices(services);

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var mediatorHandler = serviceProvider.GetService<IMediatorHandler>();
            Assert.NotNull(mediatorHandler);
        }

        [Fact]
        public void RegisterServices_ShouldRegisterCustomerAppServiceAsScoped()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            NativeInjectorBootStrapper.RegisterServices(services);

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var customerAppService = serviceProvider.GetService<ICustomerAppService>();
            Assert.NotNull(customerAppService);
        }
    }
}
