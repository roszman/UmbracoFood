using System;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using UmbracoFood.Core.Models;
using UmbracoFood.Validators.AbstractValidators;
using UmbracoFood.ViewModels;
using Xunit;

namespace UmbracoFood.Tests.Validators
{
    public class AddRestaurantValidatorTests
    {
        private AddRestaurantValidator validator;

        public AddRestaurantValidatorTests()
        {
            validator = new AddRestaurantValidator();
        }
        
        [Fact]
        public void Should_have_error_when_Name_is_empty()
        {
            validator.ShouldHaveValidationErrorFor(restaurant => restaurant.Name, String.Empty);
        }

        [Fact]
        public void Should_not_have_error_when_Name_is_specified()
        {
            validator.ShouldNotHaveValidationErrorFor(restaurant => restaurant.Name, "Specified name");
        }

        [Fact]
        public void Should_have_error_when_Phone_is_empty()
        {
            validator.ShouldHaveValidationErrorFor(restaurant => restaurant.Phone, String.Empty);
        }

        [Fact]
        public void Should_not_have_error_when_Phone_is_specified()
        {
            validator.ShouldNotHaveValidationErrorFor(restaurant => restaurant.Phone, "Specified phone 12312321");
        }

        [Fact]
        public void Should_not_have_error_when_values_are_specified()
        {
            //Arrange
            var restaurant = new AddRestaurantViewModel()
            {
                MenuUrl = "http://menu.url",
                WebsiteUrl = "http://website.url",
                Name = "Name",
                Phone = "12232213321"
            };

            //Act
            ValidationResult validationResult = validator.Validate(restaurant);

            //Assert
            Assert.True(validationResult.IsValid);
        }
    }
}