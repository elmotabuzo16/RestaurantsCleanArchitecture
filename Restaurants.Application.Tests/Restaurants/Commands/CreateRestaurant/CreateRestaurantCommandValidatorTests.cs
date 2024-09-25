using Xunit;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "Test",
                Category = "Italian",
                Description = "Test Description",
                ContactEmail = "test@test.com",
                PostalCode = "12-345"
            };

            var validator = new CreateRestaurantCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validator_ForInValidCommand_ShouldHaveValidationErrors()
        {
            // Arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "Te",
                Category = "Ita",
                Description = "",
                ContactEmail = "@test.com",
                PostalCode = "12345"
            };

            var validator = new CreateRestaurantCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.Category);
            result.ShouldHaveValidationErrorFor(c => c.Description);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        }

        [Theory()]
        [InlineData("Italian")]
        [InlineData("Mexican")]
        [InlineData("Japanese")]
        [InlineData("American")]
        [InlineData("Indian")]
        public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
        {
            // Arrange
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand() { Category = category };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.Category);
        }

        [Theory()]
        [InlineData("10220")]
        [InlineData("102-20")]
        [InlineData("10 220")]
        [InlineData("10-2 20")]
        public void Validator_ForInValidPostalCode_ShouldHaveValidationErrorsForPostalCode(string postalCode)
        {
            // Arrange
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand() { PostalCode = postalCode };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        }
    }
}