using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;

namespace UmbracoFood.Services
{
    class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            restaurantRepository.AddRestaurant(restaurant);
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
