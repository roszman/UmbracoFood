using AutoMapper;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Infrastructure.Mapping
{
    public class OrderMapper : IModelMapper<Order, OrderPoco>
    {
        public Order MapToDomain(OrderPoco poco)
        {
            return Mapper.Map<Order>(poco);
        }

        public OrderPoco MapToPoco(Order domain)
        {
            return Mapper.Map<OrderPoco>(domain);
        }
    }
}