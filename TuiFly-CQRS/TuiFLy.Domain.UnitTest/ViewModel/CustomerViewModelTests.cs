using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TuiFly.Domain.Models.ViewModels;
using Xunit;

namespace TuiFLy.Domain.UnitTest.ViewModel
{

    public class CustomerViewModelTests
    {
        [Fact]
        public void Id_GetSet()
        {
            // Arrange
            var customerViewModel = new CustomerViewModel();
            var id = Guid.NewGuid();

            // Act
            customerViewModel.Id = id;

            // Assert
            Assert.Equal(id, customerViewModel.Id);
        }

        [Fact]
        public void FirstName_GetSet()
        {
            // Arrange
            var customerViewModel = new CustomerViewModel();
            var firstName = "John";

            // Act
            customerViewModel.FirstName = firstName;

            // Assert
            Assert.Equal(firstName, customerViewModel.FirstName);
        }

        [Fact]
        public void LastName_GetSet()
        {
            // Arrange
            var customerViewModel = new CustomerViewModel();
            var lastName = "Doe";

            // Act
            customerViewModel.LastName = lastName;

            // Assert
            Assert.Equal(lastName, customerViewModel.LastName);
        }

        [Fact]
        public void Email_GetSet()
        {
            // Arrange
            var customerViewModel = new CustomerViewModel();
            var email = "john.doe@example.com";

            // Act
            customerViewModel.Email = email;

            // Assert
            Assert.Equal(email, customerViewModel.Email);
        }

        [Fact]
        public void FirstName_RequiredValidation()
        {
            // Arrange
            var customerViewModel = new CustomerViewModel();
            var propertyInfo = typeof(CustomerViewModel).GetProperty("FirstName");

            // Act
            var requiredAttribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.Equal("The FirstName is Required", requiredAttribute.ErrorMessage);
        }

        [Fact]
        public void FirstName_MinLengthValidation()
        {
            // Arrange
            var customerViewModel = new CustomerViewModel();
            var propertyInfo = typeof(CustomerViewModel).GetProperty("FirstName");

            // Act
            var minLengthAttribute = propertyInfo.GetCustomAttribute<MinLengthAttribute>();

            // Assert
            Assert.NotNull(minLengthAttribute);
            Assert.Equal(2, minLengthAttribute.Length);
        }

        [Fact]
        public void FirstName_MaxLengthValidation()
        {
            // Arrange
            var customerViewModel = new CustomerViewModel();
            var propertyInfo = typeof(CustomerViewModel).GetProperty("FirstName");

            // Act
            var maxLengthAttribute = propertyInfo.GetCustomAttribute<MaxLengthAttribute>();

            // Assert
            Assert.NotNull(maxLengthAttribute);
            Assert.Equal(100, maxLengthAttribute.Length);
        }

        [Fact]
        public void LastName_RequiredValidation()
        {
            // Arrange
            var customerViewModel = new CustomerViewModel();
            var propertyInfo = typeof(CustomerViewModel).GetProperty("LastName");

            // Act
            var requiredAttribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.Equal("The LastName is Required", requiredAttribute.ErrorMessage);
        }

        [Fact]
        public void LastName_MinLengthValidation()
        {
            // Arrange
            var customerViewModel = new CustomerViewModel();
            var propertyInfo = typeof(CustomerViewModel).GetProperty("LastName");

            // Act
            var minLengthAttribute = propertyInfo.GetCustomAttribute<MinLengthAttribute>();

            // Assert
            Assert.NotNull(minLengthAttribute);
            Assert.Equal(2, minLengthAttribute.Length);
        }

        [Fact]
        public void LastName_MaxLengthValidation()
        {
            // Arrange
            var customerViewModel = new CustomerViewModel();
            var propertyInfo = typeof(CustomerViewModel).GetProperty("LastName");

            // Act
            var maxLengthAttribute = propertyInfo.GetCustomAttribute<MaxLengthAttribute>();

            // Assert
            Assert.NotNull(maxLengthAttribute);
            Assert.Equal(100, maxLengthAttribute.Length);
        }

        [Fact]
        public void Email_RequiredValidation()
        {
            // Arrange
            var customerViewModel = new CustomerViewModel();
            var propertyInfo = typeof(CustomerViewModel).GetProperty("Email");

            // Act
            var requiredAttribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.NotNull(requiredAttribute);
            Assert.Equal("The E-mail is Required", requiredAttribute.ErrorMessage);
        }

        [Fact]
        public void Email_EmailAddressValidation()
        {
            // Arrange
            var customerViewModel = new CustomerViewModel();
            var propertyInfo = typeof(CustomerViewModel).GetProperty("Email");

            // Act
            var emailAddressAttribute = propertyInfo.GetCustomAttribute<EmailAddressAttribute>();

            // Assert
            Assert.NotNull(emailAddressAttribute);
        }

        [Fact]
        public void DisplayName_Attributes()
        {
            // Arrange
            var customerViewModel = new CustomerViewModel();
            var firstNameProperty = typeof(CustomerViewModel).GetProperty("FirstName");
            var lastNameProperty = typeof(CustomerViewModel).GetProperty("LastName");
            var emailProperty = typeof(CustomerViewModel).GetProperty("Email");

            // Act
            var firstNameDisplayName = firstNameProperty.GetCustomAttribute<DisplayNameAttribute>();
            var lastNameDisplayName = lastNameProperty.GetCustomAttribute<DisplayNameAttribute>();
            var emailDisplayName = emailProperty.GetCustomAttribute<DisplayNameAttribute>();

            // Assert
            Assert.NotNull(firstNameDisplayName);
            Assert.NotNull(lastNameDisplayName);
            Assert.NotNull(emailDisplayName);
            Assert.Equal("FirstName", firstNameDisplayName.DisplayName);
            Assert.Equal("LastName", lastNameDisplayName.DisplayName);
            Assert.Equal("E-mail", emailDisplayName.DisplayName);
        }
    }
}
