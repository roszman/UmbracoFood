using System;
using FluentValidation.Validators;
using Umbraco.Web.Models.ContentEditing;

namespace UmbracoFood.Infrastructure.Validators.PropertyValidators
{
    public class UrlValidator : PropertyValidator
    {
        public UrlValidator()
            : base("Invalid url")
        {
            
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var uriString = context.PropertyValue as string;

            Uri uriResult;

            bool result = Uri.TryCreate(uriString, UriKind.Absolute, out uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;

        }
    }
}