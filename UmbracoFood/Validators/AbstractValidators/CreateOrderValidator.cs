using System;
using FluentValidation;
using UmbracoFood.ViewModels;

namespace UmbracoFood.Validators.AbstractValidators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderViewModel>
    {
        public CreateOrderValidator()
        {
           RuleFor(r => r.Deadline).NotEmpty().GreaterThanOrEqualTo(DateTime.Now);
           RuleFor(r => r.SelectedRestaurantId).GreaterThan(0);
        }
    }
}