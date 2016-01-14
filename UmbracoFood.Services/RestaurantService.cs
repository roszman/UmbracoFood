using System.Collections.Generic;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;

namespace UmbracoFood.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }

        public int AddRestaurant(Restaurant restaurant)
        {
            return restaurantRepository.AddRestaurant(restaurant);
        }

        public void EditRestaurant(Restaurant restaurant)
        {
            restaurantRepository.EditRestaurant(restaurant);
        }

        public void RemoveRestaurant(int id)
        {
            restaurantRepository.RemoveRestaurant(id);
        }

        public Restaurant GetRestaurant(int id)
        {
            return restaurantRepository.GetRestaurant(id);
        }

        public IEnumerable<Restaurant> GetActiveRestaurants()
        {
            return restaurantRepository.GetActiveRestaurants();
        }

        public IEnumerable<Restaurant> GetInactiveRestaurants()
        {
            return restaurantRepository.GetInactiveRestaurants();
        }
    }
}
