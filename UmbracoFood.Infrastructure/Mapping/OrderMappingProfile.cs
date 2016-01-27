using System.Linq;
using AutoMapper;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Infrastructure.Mapping
{
    public class OrderMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<OrderPoco, Order>()

                .ForMember(d => d.Status, o => o.MapFrom(s => (OrderStatus) s.StatusId))
                .ForMember(d => d.Restaurant, o => o.MapFrom(s => s.Restaurant))
                .ForMember(d => d.OrderedMeals, o => o.MapFrom(s => s.OrderedMeals))
                ;
        

        CreateMap<Order, OrderPoco>()
                .ForMember(d => d.StatusId, o => o.MapFrom(s => (int) s.Status))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
                .ForMember(d => d.Restaurant, o => o.MapFrom(s => new RestaurantPoco(){ID = s.Restaurant.ID}))
                ;
        }
    }
}