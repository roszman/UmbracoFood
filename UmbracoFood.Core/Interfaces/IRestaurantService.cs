using System.Collections.Generic;
using UmbracoFood.Core.Models;

namespace UmbracoFood.Core.Interfaces
{
    public interface IRestaurantService
    {
        IEnumerable<Restaurant> GetActiveRestaurants();
    }
}