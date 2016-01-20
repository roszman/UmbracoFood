using System;
using System.Web.Http;
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

        [HttpPost]
        public void PostRestaurant(AddRestaurantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Couldn't add a restaurant");
            }

            var restaurant = Mapper.Map<AddRestaurantViewModel, Restaurant>(model);
            restaurantService.AddRestaurant(restaurant);
        }

        public GetRestaurantsResult GetRestaurants()
        {
            var restaurants = restaurantService.GetActiveRestaurants();

            var getRestaurantsResult = new GetRestaurantsResult()
            {
                Restaurants = restaurants.Select(Mapper.Map<Core.Models.Restaurant, RestaurantViewModel>)
            };

            return getRestaurantsResult;
        }

        public EditRestaurantViewModel GetRestaurant(int id)
        {
            if (id < 1)
            {
                throw new Exception("ID nie może być niższe niż 1");
            }

            var restaurant = restaurantService.GetRestaurant(id);
            if (restaurant == null)
            {
                throw new Exception("Restaurant doesn't exist.");
            }

            return Mapper.Map<Core.Models.Restaurant, EditRestaurantViewModel>(restaurant);
        }

        [HttpPut]
        public void PutRestaurant(EditRestaurantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Nie udało się zedytować restauracji");
            }

            var restaurant = restaurantService.GetRestaurant(model.ID);
            if (restaurant == null)
            {
                throw new Exception("Restauracja nie istnieje");
            }

            var updatedRestaurant = Mapper.Map<EditRestaurantViewModel, Restaurant>(model, restaurant);
            restaurantService.EditRestaurant(updatedRestaurant);
        }
    }
}