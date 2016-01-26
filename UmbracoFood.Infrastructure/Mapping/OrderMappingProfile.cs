using AutoMapper;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Infrastructure.Mapping
{
    public class OrderMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<OrderPoco, Order>().ReverseMap();
        }
    }
}