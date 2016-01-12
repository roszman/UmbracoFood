using System.Collections.Generic;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;

namespace UmbracoFood.Infrastructure2.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        public void AddRestaurant(Restaurant restaurant)
        {
            throw new System.NotImplementedException();
        }

        public void EditRestaurant(Restaurant restaurant)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveRestaurant(int id)
        {
            throw new System.NotImplementedException();
        }

        public Restaurant GetRestaurant(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Restaurant> GetActiveRestaurants()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Restaurant> GetInactiveRestaurants()
        {
            throw new System.NotImplementedException();
        }
    }
}