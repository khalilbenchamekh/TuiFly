using TuiFly.Domain.Models.Entities;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Models.Entities
{
    public class PlanEntityTests
    {
        [Fact]
        public void Constructor_WithValidParameters_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var modelName = "Boeing 747";
            var flights = new List<FlightEntity>
        {
            new FlightEntity(),
            new FlightEntity(),
            new FlightEntity()
        };

            // Act
            var plan = new PlanEntity
            {
                Id = id,
                ModelName = modelName,
                Flights = flights
            };

            // Assert
            Assert.Equal(id, plan.Id);
            Assert.Equal(modelName, plan.ModelName);
            Assert.Equal(flights, plan.Flights);
        }

        [Fact]
        public void Constructor_WithDefaultValues_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.Empty;
            var modelName = "";
            var flights = new List<FlightEntity>();

            // Act
            var plan = new PlanEntity
            {
                Id = id,
                ModelName = modelName,
                Flights = flights
            };

            // Assert
            Assert.Equal(id, plan.Id);
            Assert.Equal(modelName, plan.ModelName);
            Assert.Equal(flights, plan.Flights);
        }
    }
}
