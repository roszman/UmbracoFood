using AutoMapper;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Infrastructure.Mapping
{
    public class RestaurantRepositoryMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<RestaurantPoco, Restaurant>().ReverseMap();
        }
    }
}