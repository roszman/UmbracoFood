using System;
using Umbraco.Web.WebApi;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;

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
    }
} ;