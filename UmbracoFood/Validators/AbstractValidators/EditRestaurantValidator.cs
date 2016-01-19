using System.Web.Http;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Mvc;
using FluentValidation.Results;
using UmbracoFood.Core.Models;
using UmbracoFood.Validators.PropertyValidators;
using UmbracoFood.ViewModels;

namespace UmbracoFood.Validators.AbstractValidators
{
    public class EditRestaurantValidator : AbstractValidator<EditRestaurantViewModel>
    {
        public EditRestaurantValidator()
        {
           RuleFor(r => r.ID).GreaterThan(0);
           RuleFor(r => r.MenuUrl).SetValidator(new UrlValidator());
           RuleFor(r => r.WebsiteUrl).SetValidator(new UrlValidator());
           RuleFor(r => r.Name).NotEmpty();
           RuleFor(r => r.Phone).NotEmpty();
        }
    }
}