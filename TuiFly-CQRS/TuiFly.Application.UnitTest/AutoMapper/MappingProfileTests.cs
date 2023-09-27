using AutoMapper;
using TuiFly.Application.AutoMapper;
using Xunit;

namespace TuiFly.Application.UnitTest.AutoMapper
{
    public class MappingProfileTests
    {
        [Fact]
        public void DomainToViewModelMappings_AreValid()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
            });

            // Act and Assert
            configuration.AssertConfigurationIsValid();
        }
    }
}
