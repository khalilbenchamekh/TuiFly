using AutoMapper;
using TuiFly.Application.AutoMapper;
using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Commands.Flight.Validation;
using TuiFly.Domain.Models.ViewModels;
using Xunit;

namespace TuiFly.Application.UnitTest.AutoMapper
{
    public class ViewModelToDomainMappingProfileTests
    {
        [Fact]
        public void ShouldMap_ReservationViewModel_To_MakeReservationCommandValidation()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });
            var mapper = configuration.CreateMapper();

            // Act
            var source = new ReservationViewModel();
            var destination = mapper.Map<MakeReservationCommandValidation>(source);

            // Assert
            // Add assertions to verify that the properties are correctly mapped
            Assert.NotNull(destination);
            Assert.Equal(source.FlightId, destination.FlightId); // Replace with actual property names
                                                                   // Add more assertions for other properties
        }

        [Fact]
        public void ShouldMap_CustomerViewModel_To_RegisterNewCustomerCommand()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });
            var mapper = configuration.CreateMapper();

            // Act
            var source = new CustomerViewModel();
            var destination = mapper.Map<RegisterNewCustomerCommand>(source);

            // Assert
            // Add assertions to verify that the properties are correctly mapped
            Assert.NotNull(destination);
            Assert.Equal(source.LastName, destination.LastName); // Replace with actual property names
                                                                 // Add more assertions for other properties
        }

        // Add similar test methods for other mappings...

    }
}
