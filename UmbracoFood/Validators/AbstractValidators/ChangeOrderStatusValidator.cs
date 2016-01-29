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
    public class ChangeOrderStatusValidator : AbstractValidator<EditOrderViewModel>
    {
        public ChangeOrderStatusValidator()
        {
            RuleFor(r => r.OrderId).GreaterThan(0);
            RuleFor(r => r.Status).InclusiveBetween(1, 3);

            RuleFor(r => r.EstimatedDeliveryTime)
                .NotEmpty()
                .When(o => (OrderStatus) o.Status == OrderStatus.InDelivery);
        }
    }
}