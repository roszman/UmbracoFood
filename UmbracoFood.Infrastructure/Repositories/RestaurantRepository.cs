using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Infrastructure.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly IModelMapper<Restaurant, RestaurantPoco> _mapper;
        private UmbracoDatabase _db;

        public RestaurantRepository(IDatabaseProvider databaseProvider, IModelMapper<Restaurant, RestaurantPoco> mapper)
        {
            _databaseProvider = databaseProvider;
            _mapper = mapper;
            _db = databaseProvider.Db;
        }

        public int AddRestaurant(Restaurant restaurant)
        {
            var poco = _mapper.MapToPoco(restaurant);

            var id = _db.Insert("Restaurants", "Id", poco);
            return decimal.ToInt32((decimal)id);
        }

        public void EditRestaurant(Restaurant restaurant)
        {
            _db.Update("Restaurants", "Id", _mapper.MapToPoco(restaurant));
        }

        public void RemoveRestaurant(int id)
        {
            var restaurant = GetRestaurantPoco(id);
            restaurant.IsActive = false;
            _db.Update("Restaurants", "Id", restaurant);
        }

        public Restaurant GetRestaurant(int id)
        {
            var restaurant = GetRestaurantPoco(id);
            return _mapper.MapToDomain(restaurant);
        }

        private RestaurantPoco GetRestaurantPoco(int id)
        {
            var restaurant = _db.SingleOrDefault<RestaurantPoco>("SELECT * FROM Restaurants WHERE Id = @0", id);
            if (restaurant == null)
            {
                throw new KeyNotFoundException(string.Format("Restaurant {0} not found in database", id));
            }
            return restaurant;
        }

        public IEnumerable<Restaurant> GetActiveRestaurants()
        {
            var restaurants = _db.Query<RestaurantPoco>("SELECT * FROM Restaurants WHERE Active = 1");
            return restaurants.Select(r => _mapper.MapToDomain(r));
        }

        public IEnumerable<Restaurant> GetInactiveRestaurants()
        {
            var restaurants = _db.Query<RestaurantPoco>("SELECT * FROM Restaurants WHERE Active = 0");
            return restaurants.Select(r => _mapper.MapToDomain(r));
        }
    }
}