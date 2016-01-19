using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Umbraco.Core;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Infrastructure.Repositories
{
    public class RestaurantRepository : BaseRepository, IRestaurantRepository
    {
        public int AddRestaurant(Restaurant restaurant)
        {
            var id = db.Insert("Restaurants", "Id", Mapper.Map<Restaurant, RestaurantPoco>(restaurant));
            return decimal.ToInt32((decimal)id);
        }

        public void EditRestaurant(Restaurant restaurant)
        {
            var updatedRestaurant = db.SingleOrDefault<RestaurantPoco>("SELECT * FROM Restaurants WHERE Id = @0", restaurant.ID);
            if (updatedRestaurant == null)
            {
                throw new KeyNotFoundException("Restaurant has not been found.");
            }

            db.Update("Restaurants", "Id", Mapper.Map<Restaurant, RestaurantPoco>(restaurant, updatedRestaurant));
        }

        public void RemoveRestaurant(int id)
        {
            var restaurant = GetRestaurant(id);
            db.Execute("DELETE FROM Restaurants WHERE Id = @0", id);
        }

        public Restaurant GetRestaurant(int id)
        {
            var restaurant = db.SingleOrDefault<RestaurantPoco>("SELECT * FROM Restaurants WHERE Id = @0", id);
            if (restaurant == null)
            {
                throw new KeyNotFoundException("Restaurant has not been found.");
            }
            return Mapper.Map<Restaurant>(restaurant);
        }

        public IEnumerable<Restaurant> GetActiveRestaurants()
        {
            var restaurants = db.Query<RestaurantPoco>("SELECT * FROM Restaurants WHERE Active = 1");
            return restaurants.Select(Mapper.Map<Restaurant>);
        }

        public IEnumerable<Restaurant> GetInactiveRestaurants()
        {
            var restaurants = db.Query<RestaurantPoco>("SELECT * FROM Restaurants WHERE Active = 0");
            return restaurants.Select(Mapper.Map<Restaurant>);
        }
    }
}