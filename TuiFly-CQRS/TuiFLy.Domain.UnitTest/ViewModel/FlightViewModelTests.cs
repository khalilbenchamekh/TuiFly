using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TuiFly.Domain.Models.ViewModels;
using Xunit;

namespace TuiFLy.Domain.UnitTest.ViewModel
{
    public class FlightViewModelTests
    {
        [Fact]
        public void Id_GetSet()
        {
            // Arrange
            var flightViewModel = new FlightViewModel();
            var id = Guid.NewGuid();

            // Act
            flightViewModel.Id = id;

            // Assert
            Assert.Equal(id, flightViewModel.Id);
        }

        [Fact]
        public void DepartureCity_GetSet()
        {
            // Arrange
            var flightViewModel = new FlightViewModel();
            var departureCity = "New York";

            // Act
            flightViewModel.DepartureCity = departureCity;

            // Assert
            Assert.Equal(departureCity, flightViewModel.DepartureCity);
        }

        [Fact]
        public void ArrivalCity_GetSet()
        {
            // Arrange
            var flightViewModel = new FlightViewModel();
            var arrivalCity = "Los Angeles";

            // Act
            flightViewModel.ArrivalCity = arrivalCity;

            // Assert
            Assert.Equal(arrivalCity, flightViewModel.ArrivalCity);
        }

        [Fact]
        public void DepartureDate_GetSet()
        {
            // Arrange
            var flightViewModel = new FlightViewModel();
            var departureDate = DateTime.Now;

            // Act
            flightViewModel.DepartureDate = departureDate;

            // Assert
            Assert.Equal(departureDate, flightViewModel.DepartureDate);
        }

        [Fact]
        public void ArrivalDate_GetSet()
        {
            // Arrange
            var flightViewModel = new FlightViewModel();
            var arrivalDate = DateTime.Now;

            // Act
            flightViewModel.ArrivalDate = arrivalDate;

            // Assert
            Assert.Equal(arrivalDate, flightViewModel.ArrivalDate);
        }

        [Fact]
        public void PageNumber_GetSet()
        {
            // Arrange
            var flightViewModel = new FlightViewModel();
            var pageNumber = 2;

            // Act
            flightViewModel.PageNumber = pageNumber;

            // Assert
            Assert.Equal(pageNumber, flightViewModel.PageNumber);
        }

        [Fact]
        public void PageSize_GetSet()
        {
            // Arrange
            var flightViewModel = new FlightViewModel();
            var pageSize = 20;

            // Act
            flightViewModel.PageSize = pageSize;

            // Assert
            Assert.Equal(pageSize, flightViewModel.PageSize);
        }

        [Fact]
        public void DepartureCity_RequiredValidation()
        {
            // Arrange
            var flightViewModel = new FlightViewModel();
            var propertyInfo = typeof(FlightViewModel).GetProperty("DepartureCity");

            // Act
            var requiredAttribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.Equal("The DepartureCity is Required", requiredAttribute.ErrorMessage);
        }

        [Fact]
        public void ArrivalCity_RequiredValidation()
        {
            // Arrange
            var flightViewModel = new FlightViewModel();
            var propertyInfo = typeof(FlightViewModel).GetProperty("ArrivalCity");

            // Act
            var requiredAttribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.Equal("The ArrivalCity is Required", requiredAttribute.ErrorMessage);
        }

        [Fact]
        public void DepartureDate_RequiredValidation()
        {
            // Arrange
            var flightViewModel = new FlightViewModel();
            var propertyInfo = typeof(FlightViewModel).GetProperty("DepartureDate");

            // Act
            var requiredAttribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.Equal("The DepartureDate is Required", requiredAttribute.ErrorMessage);
        }

        [Fact]
        public void PageNumber_RangeValidation()
        {
            // Arrange
            var flightViewModel = new FlightViewModel();
            var propertyInfo = typeof(FlightViewModel).GetProperty("PageNumber");

            // Act
            var rangeAttribute = propertyInfo.GetCustomAttribute<RangeAttribute>();

            // Assert
            Assert.NotNull(rangeAttribute);
            Assert.Equal(1, rangeAttribute.Minimum);
            Assert.Equal(int.MaxValue, rangeAttribute.Maximum);
            Assert.Equal("PageNumber must be greater than or equal to 1", rangeAttribute.ErrorMessage);
        }

        [Fact]
        public void PageSize_RangeValidation()
        {
            // Arrange
            var flightViewModel = new FlightViewModel();
            var propertyInfo = typeof(FlightViewModel).GetProperty("PageSize");

            // Act
            var rangeAttribute = propertyInfo.GetCustomAttribute<RangeAttribute>();

            // Assert
            Assert.NotNull(rangeAttribute);
            Assert.Equal(1, rangeAttribute.Minimum);
            Assert.Equal(int.MaxValue, rangeAttribute.Maximum);
            Assert.Equal("PageSize must be greater than or equal to 1", rangeAttribute.ErrorMessage);
        }

        [Fact]
        public void DisplayName_Attributes()
        {
            // Arrange
            var flightViewModel = new FlightViewModel();
            var departureCityProperty = typeof(FlightViewModel).GetProperty("DepartureCity");
            var arrivalCityProperty = typeof(FlightViewModel).GetProperty("ArrivalCity");
            var departureDateProperty = typeof(FlightViewModel).GetProperty("DepartureDate");
            var pageNumberProperty = typeof(FlightViewModel).GetProperty("PageNumber");
            var pageSizeProperty = typeof(FlightViewModel).GetProperty("PageSize");

            // Act
            var departureCityDisplayName = departureCityProperty.GetCustomAttribute<DisplayNameAttribute>();
            var arrivalCityDisplayName = arrivalCityProperty.GetCustomAttribute<DisplayNameAttribute>();
            var departureDateDisplayName = departureDateProperty.GetCustomAttribute<DisplayNameAttribute>();
            var pageNumberDisplayName = pageNumberProperty.GetCustomAttribute<DisplayNameAttribute>();
            var pageSizeDisplayName = pageSizeProperty.GetCustomAttribute<DisplayNameAttribute>();

            // Assert
            Assert.NotNull(departureCityDisplayName);
            Assert.NotNull(arrivalCityDisplayName);
            Assert.NotNull(departureDateDisplayName);
            Assert.NotNull(pageNumberDisplayName);
            Assert.NotNull(pageSizeDisplayName);

            Assert.Equal("DepartureCity", departureCityDisplayName.DisplayName);
            Assert.Equal("ArrivalCity", arrivalCityDisplayName.DisplayName);
            Assert.Equal("DepartureDate", departureDateDisplayName.DisplayName);
            Assert.Equal("PageNumber", pageNumberDisplayName.DisplayName);
            Assert.Equal("PageSize", pageSizeDisplayName.DisplayName);
        }
    }
}
