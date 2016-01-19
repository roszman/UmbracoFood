using AutoMapper;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;
using UmbracoFood.Models;

namespace UmbracoFood.Mapping
{
    public class RestaurantMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<RestaurantViewModel, Restaurant>()
                .ForMember(d => d.IsActive, s => s.UseValue(true));
        }
    }
}