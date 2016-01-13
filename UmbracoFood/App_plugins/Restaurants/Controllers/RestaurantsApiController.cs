using System.Collections.Generic;
using System.Web.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;

namespace UmbracoFood.App_plugins.Restaurants.Controllers
{
    [PluginController("Restaurants")]
    public class RestaurantsApiController : UmbracoAuthorizedApiController
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsApiController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        public IEnumerable<Restaurant> GetActiveRestaurants()
        {
            IEnumerable<Restaurant> activeRestaurants = _restaurantService.GetActiveRestaurants();
            return activeRestaurants;
        }
        public IEnumerable<Restaurant> GetinactiveRestaurants()
        {
            IEnumerable<Restaurant> inactiveRestaurants = _restaurantService.GetInactiveRestaurants();
            return inactiveRestaurants;
        }

        public Restaurant GetById(int id)
        {
            Restaurant restaurant = _restaurantService.GetRestaurant(id);
            return restaurant;
        }

        public void PostUpdate([FromBody]Restaurant restaurant)
        {
            _restaurantService.AddRestaurant(restaurant);
        }

        public void PutCreate([FromBody]Restaurant restaurant)
        {

        }

        public void Delete(int id)
        {
            _restaurantService.RemoveRestaurant(id);
        }
    }
}
