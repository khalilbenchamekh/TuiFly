using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TuiFly.Domain.Models.ViewModels;
using Xunit;

namespace TuiFLy.Domain.UnitTest.ViewModel
{
    public class PassengerViewModelTests
    {
        [Fact]
        public void LastName_GetSet()
        {
            // Arrange
            var passengerViewModel = new PassengerViewModel();
            var lastName = "Smith";

            // Act
            passengerViewModel.LastName = lastName;

            // Assert
            Assert.Equal(lastName, passengerViewModel.LastName);
        }

        [Fact]
        public void FirstName_GetSet()
        {
            // Arrange
            var passengerViewModel = new PassengerViewModel();
            var firstName = "John";

            // Act
            passengerViewModel.FirstName = firstName;

            // Assert
            Assert.Equal(firstName, passengerViewModel.FirstName);
        }

        [Fact]
        public void Email_GetSet()
        {
            // Arrange
            var passengerViewModel = new PassengerViewModel();
            var email = "john.smith@example.com";

            // Act
            passengerViewModel.Email = email;

            // Assert
            Assert.Equal(email, passengerViewModel.Email);
        }

        [Fact]
        public void LastName_RequiredValidation()
        {
            // Arrange
            var passengerViewModel = new PassengerViewModel();
            var propertyInfo = typeof(PassengerViewModel).GetProperty("LastName");

            // Act
            var requiredAttribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.Equal("The LastName is required.", requiredAttribute.ErrorMessage);
        }

        [Fact]
        public void LastName_MinLengthValidation()
        {
            // Arrange
            var passengerViewModel = new PassengerViewModel();
            var propertyInfo = typeof(PassengerViewModel).GetProperty("LastName");

            // Act
            var minLengthAttribute = propertyInfo.GetCustomAttribute<MinLengthAttribute>();

            // Assert
            Assert.NotNull(minLengthAttribute);
            Assert.Equal(2, minLengthAttribute.Length);
            Assert.Equal("The LastName must be at least 2 characters.", minLengthAttribute.ErrorMessage);
        }

        [Fact]
        public void LastName_MaxLengthValidation()
        {
            // Arrange
            var passengerViewModel = new PassengerViewModel();
            var propertyInfo = typeof(PassengerViewModel).GetProperty("LastName");

            // Act
            var maxLengthAttribute = propertyInfo.GetCustomAttribute<MaxLengthAttribute>();

            // Assert
            Assert.NotNull(maxLengthAttribute);
            Assert.Equal(100, maxLengthAttribute.Length);
            Assert.Equal("The LastName cannot exceed 100 characters.", maxLengthAttribute.ErrorMessage);
        }

        [Fact]
        public void FirstName_RequiredValidation()
        {
            // Arrange
            var passengerViewModel = new PassengerViewModel();
            var propertyInfo = typeof(PassengerViewModel).GetProperty("FirstName");

            // Act
            var requiredAttribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.Equal("The FirstName is required.", requiredAttribute.ErrorMessage);
        }

        [Fact]
        public void FirstName_MinLengthValidation()
        {
            // Arrange
            var passengerViewModel = new PassengerViewModel();
            var propertyInfo = typeof(PassengerViewModel).GetProperty("FirstName");

            // Act
            var minLengthAttribute = propertyInfo.GetCustomAttribute<MinLengthAttribute>();

            // Assert
            Assert.NotNull(minLengthAttribute);
            Assert.Equal(2, minLengthAttribute.Length);
            Assert.Equal("The FirstName must be at least 2 characters.", minLengthAttribute.ErrorMessage);
        }

        [Fact]
        public void FirstName_MaxLengthValidation()
        {
            // Arrange
            var passengerViewModel = new PassengerViewModel();
            var propertyInfo = typeof(PassengerViewModel).GetProperty("FirstName");

            // Act
            var maxLengthAttribute = propertyInfo.GetCustomAttribute<MaxLengthAttribute>();

            // Assert
            Assert.NotNull(maxLengthAttribute);
            Assert.Equal(100, maxLengthAttribute.Length);
            Assert.Equal("The FirstName cannot exceed 100 characters.", maxLengthAttribute.ErrorMessage);
        }

        [Fact]
        public void Email_RequiredValidation()
        {
            // Arrange
            var passengerViewModel = new PassengerViewModel();
            var propertyInfo = typeof(PassengerViewModel).GetProperty("Email");

            // Act
            var requiredAttribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.Equal("The Email is required.", requiredAttribute.ErrorMessage);
        }

        [Fact]
        public void Email_EmailAddressValidation()
        {
            // Arrange
            var passengerViewModel = new PassengerViewModel();
            var propertyInfo = typeof(PassengerViewModel).GetProperty("Email");

            // Act
            var emailAddressAttribute = propertyInfo.GetCustomAttribute<EmailAddressAttribute>();

            // Assert
            Assert.NotNull(emailAddressAttribute);
            Assert.Equal("Invalid email format.", emailAddressAttribute.ErrorMessage);
        }

        [Fact]
        public void DisplayName_Attributes()
        {
            // Arrange
            var passengerViewModel = new PassengerViewModel();
            var lastNameProperty = typeof(PassengerViewModel).GetProperty("LastName");
            var firstNameProperty = typeof(PassengerViewModel).GetProperty("FirstName");
            var emailProperty = typeof(PassengerViewModel).GetProperty("Email");

            // Act
            var lastNameDisplayName = lastNameProperty.GetCustomAttribute<DisplayNameAttribute>();
            var firstNameDisplayName = firstNameProperty.GetCustomAttribute<DisplayNameAttribute>();
            var emailDisplayName = emailProperty.GetCustomAttribute<DisplayNameAttribute>();

            // Assert
            Assert.NotNull(lastNameDisplayName);
            Assert.NotNull(firstNameDisplayName);
            Assert.NotNull(emailDisplayName);

            Assert.Equal("LastName", lastNameDisplayName.DisplayName);
            Assert.Equal("FirstName", firstNameDisplayName.DisplayName);
            Assert.Equal("Email", emailDisplayName.DisplayName);
        }
    }
}
