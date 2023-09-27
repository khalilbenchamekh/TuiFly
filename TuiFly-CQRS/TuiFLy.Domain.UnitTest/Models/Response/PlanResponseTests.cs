using TuiFly.Domain.Models.Response;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Models.Response
{
    public class PlanResponseTests
    {
        [Fact]
        public void Constructor_WithValidParameters_SetsProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var modelName = "Model1";
            var flights = new List<FlightResponse> { new FlightResponse() };

            // Act
            var planResponse = new PlanResponse
            {
                Id = id,
                ModelName = modelName,
                Flights = flights
            };

            // Assert
            Assert.Equal(id, planResponse.Id);
            Assert.Equal(modelName, planResponse.ModelName);
            Assert.Equal(flights, planResponse.Flights);
        }

        [Fact]
        public void Constructor_WithDefaultValues_SetsProperties()
        {
            // Arrange

            // Act
            var planResponse = new PlanResponse();

            // Assert
            Assert.Equal(Guid.Empty, planResponse.Id);
            Assert.Null(planResponse.ModelName);
            Assert.Null(planResponse.Flights);
        }
    }
}
