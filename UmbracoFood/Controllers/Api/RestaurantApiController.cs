using System;
using AutoMapper;
using Umbraco.Web.WebApi;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;
using System.Linq;
using UmbracoFood.ViewModels;

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

        public void PostRestaurant(AddRestaurantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Couldn't add a restaurant");
            }
            
            var restaurant = Mapper.Map<AddRestaurantViewModel, Restaurant>(model);
            restaurantService.AddRestaurant(restaurant);
        }

      
        public GetRestaurantsResult GetRestaurant()
        {
            var restaurants = restaurantService.GetActiveRestaurants();

            return new GetRestaurantsResult()
            {
                Restaurants = restaurants.Select(Mapper.Map<Core.Models.Restaurant, RestaurantViewModel>)
            };
        }

        public void PutRestaurant(EditRestaurantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Couldn't edit a restaurant");
            }

            var restaurant = Mapper.Map<EditRestaurantViewModel, Restaurant>(model);
            restaurantService.EditRestaurant(restaurant);
        }






    }
} ;