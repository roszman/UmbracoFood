using AutoMapper;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;
using UmbracoFood.Infrastructure.Repositories;

namespace UmbracoFood.Infrastructure.Mapping
{
    public class RestaurantMapper : IModelMapper<Restaurant, RestaurantPoco>
    {
        private readonly IMappingEngine _mappingEngine;

        public RestaurantMapper(IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
        }

        public Restaurant MapToDomain(RestaurantPoco poco)
        {
            return _mappingEngine.Map<Restaurant>(poco);
        }

        public RestaurantPoco MapToPoco(Restaurant domain)
        {
            return _mappingEngine.Map<RestaurantPoco>(domain);
        }
    }
}