using AutoMapper;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Infrastructure.Mapping
{
    public class RestaurantRepositoryMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<RestaurantPoco, Restaurant>()
                .ForMember(d => d.ID, s => s.MapFrom(o => o.Id))
                .ForMember(d => d.MenuUrl, s => s.MapFrom(o => o.MenuUrl))
                .ForMember(d => d.Name, s => s.MapFrom(o => o.Name))
                .ForMember(d => d.Phone, s => s.MapFrom(o => o.Phone))
                .ForMember(d => d.WebsiteUrl, s => s.MapFrom(o => o.Url))
                .ForMember(d => d.IsActive, s => s.MapFrom(o => o.IsActive));

            CreateMap<Restaurant, RestaurantPoco>()
                .ForMember(d => d.Id, s => s.MapFrom(o => o.ID))
                .ForMember(d => d.MenuUrl, s => s.MapFrom(o => o.MenuUrl))
                .ForMember(d => d.Name, s => s.MapFrom(o => o.Name))
                .ForMember(d => d.Phone, s => s.MapFrom(o => o.Phone))
                .ForMember(d => d.Url, s => s.MapFrom(o => o.WebsiteUrl))
                .ForMember(d => d.Active, s => s.Ignore());

        }
    }
}