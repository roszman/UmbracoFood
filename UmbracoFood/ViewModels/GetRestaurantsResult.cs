using System.Collections.Generic;

namespace UmbracoFood.ViewModels
{
    public class GetRestaurantsResult
    {
        public IEnumerable<RestaurantViewModel> Restaurants { get; set; }
    }
}