using System;
using FluentValidation.Validators;

namespace UmbracoFood.Validators.PropertyValidators
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