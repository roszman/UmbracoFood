using AutoMapper;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;
using UmbracoFood.Infrastructure.Repositories;

namespace UmbracoFood.Infrastructure.Mapping
{
    public class RestaurantMapper : IModelMapper<Restaurant, RestaurantPoco>
    {
        public Restaurant MapToDomain(RestaurantPoco poco)
        {
            return Mapper.Map<Restaurant>(poco);
        }

        public RestaurantPoco MapToPoco(Restaurant domain)
        {
            return Mapper.Map<RestaurantPoco>(domain);
        }
    }
}