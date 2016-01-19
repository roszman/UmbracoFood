using System;
using AutoMapper;
using Umbraco.Web.WebApi;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;
using UmbracoFood.Models;

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

        public void PostRestaurant(RestaurantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Couldn't add a restaurant");
            }
            
            var restaurant = Mapper.Map<RestaurantViewModel, Restaurant>(model);
            restaurantService.AddRestaurant(restaurant);
        }

        public void PutRestaurant(RestaurantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Couldn't edit a restaurant");
            }

            var restaurant = Mapper.Map<RestaurantViewModel, Restaurant>(model);
            restaurantService.EditRestaurant(restaurant);
        }






    }
} ;