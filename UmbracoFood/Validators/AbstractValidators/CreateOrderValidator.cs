using System;
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
    public class CreateOrderValidator : AbstractValidator<CreateOrderViewModel>
    {
        public CreateOrderValidator()
        {
           RuleFor(r => r.Deadline).NotEmpty().GreaterThanOrEqualTo(DateTime.Now);
           RuleFor(r => r.Owner).NotEmpty();
           RuleFor(r => r.SelectedRestaurantId).GreaterThan(0);
        }
    }
}