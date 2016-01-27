using AutoMapper;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Infrastructure.Mapping
{
    public class OrderedMealMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<OrderedMealPoco, OrderedMeal>()
                .ReverseMap();
        }
    }
}
