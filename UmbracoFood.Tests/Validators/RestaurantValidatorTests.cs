using System;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using UmbracoFood.Core.Models;
using UmbracoFood.Validators.AbstractValidators;
using Xunit;

namespace UmbracoFood.Tests.Validators
{
    public class RestaurantValidatorTests
    {
        private RestaurantValidator validator;

        public RestaurantValidatorTests()
        {
            validator = new RestaurantValidator();
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
            var restaurant = new Restaurant()
            {
                IsActive = true,
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


        [Fact]
        public void Should_not_have_error_when_editing_and_Id_is_specified()
        {
            //Arrange
            var restaurant = new Restaurant()
            {
                ID = 999,
                IsActive = true,
                MenuUrl = "http://menu.url",
                WebsiteUrl = "http://website.url",
                Name = "Name",
                Phone = "12232213321"
            };

            //Act
            ValidationResult validationResult = validator.Validate(restaurant, ruleSet: "default, Edit");

            //Assert
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void Should_not_have_error_when_editing_and_Id_is_not_valid()
        {
            //Arrange
            var restaurant = new Restaurant()
            {
                ID = 0,
                IsActive = true,
                MenuUrl = "http://menu.url",
                WebsiteUrl = "http://website.url",
                Name = "Name",
                Phone = "12232213321"
            };

            //Act
            ValidationResult validationResult = validator.Validate(restaurant, ruleSet: "default,Edit");

            //Assert
            Assert.False(validationResult.IsValid);
        }
    }
}