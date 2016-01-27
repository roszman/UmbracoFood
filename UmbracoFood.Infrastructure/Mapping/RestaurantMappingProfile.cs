using AutoMapper;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Infrastructure.Mapping
{
    public class RestaurantMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<RestaurantPoco, Restaurant>()
                .ReverseMap();
        }
    }
}