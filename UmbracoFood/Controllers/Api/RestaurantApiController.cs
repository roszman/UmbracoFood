using System;
using FluentValidation;
using FluentValidation.Internal;
using Umbraco.Web.WebApi;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Validators.AbstractValidators;

namespace UmbracoFood.Controllers.Api
{
    [Umbraco.Web.Mvc.PluginController("UmbracoFoodApi")]
    public class RestaurantApiController : UmbracoApiController
    {
        private readonly IRestaurantService restaurantService;

        public RestaurantApiController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        public void PostRestaurant(Restaurant model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Couldn't add a restaurant");
            }
            
            restaurantService.AddRestaurant(model);
        }

        public void PutRestaurant(Restaurant model)
        {
            var validator = new RestaurantValidator();

            var result = validator.Validate(model, ruleSet: "default,Edit");
            if (!result.IsValid)
            {
                throw new Exception("Couldn't edit a restaurant");
            }

            restaurantService.EditRestaurant(model);
        }




    }
} ;