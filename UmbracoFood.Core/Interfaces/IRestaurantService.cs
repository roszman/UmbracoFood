﻿using System.Collections.Generic;
using UmbracoFood.Core.Models;

namespace UmbracoFood.Core.Interfaces
{
    public interface IRestaurantService
    {
        int AddRestaurant(Restaurant restaurant);

        void EditRestaurant(Restaurant restaurant);

        void RemoveRestaurant(int id);

        Restaurant GetRestaurant(int id);

        IEnumerable<Restaurant> GetActiveRestaurants();

        IEnumerable<Restaurant> GetInactiveRestaurants();

    }
}