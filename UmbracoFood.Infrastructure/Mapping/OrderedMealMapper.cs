using AutoMapper;
using System;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Infrastructure.Mapping
{
    class OrderedMealMapper : IModelMapper<OrderedMeal, OrderedMealPoco>
    {
        public OrderedMeal MapToDomain(OrderedMealPoco poco)
        {
            return Mapper.Map<OrderedMeal>(poco);
        }

        public OrderedMealPoco MapToPoco(OrderedMeal domain)
        {
            return Mapper.Map<OrderedMealPoco>(domain);
        }
    }
}
