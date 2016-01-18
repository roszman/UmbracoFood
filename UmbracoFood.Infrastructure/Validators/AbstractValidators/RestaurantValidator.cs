using FluentValidation;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Validators.PropertyValidators;

namespace UmbracoFood.Infrastructure.Validators.AbstractValidators
{
    public class RestaurantValidator : AbstractValidator<Restaurant>
    {
        public RestaurantValidator()
        {
            RuleSet("Edit", () =>
            {
                RuleFor(r => r.ID).GreaterThan(0);
            });
            
            RuleFor(r => r.MenuUrl).SetValidator(new UrlValidator());
            RuleFor(r => r.WebsiteUrl).SetValidator(new UrlValidator());
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Phone).NotEmpty();
        }
    }
}