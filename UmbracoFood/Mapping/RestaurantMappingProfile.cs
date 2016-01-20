using AutoMapper;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;
using UmbracoFood.ViewModels;

namespace UmbracoFood.Mapping
{
    public class RestaurantMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddRestaurantViewModel, Restaurant>()
                .ForMember(d => d.IsActive, s => s.UseValue(true))
                .ForMember(d => d.ID, s => s.Ignore());

            CreateMap<EditRestaurantViewModel, Restaurant>()
                .ForMember(d => d.IsActive, o => o.Ignore())
                ;


            CreateMap<Restaurant, EditRestaurantViewModel>();

            CreateMap<Restaurant, RestaurantViewModel>();
        }
    }
}