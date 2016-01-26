using AutoMapper;
using UmbracoFood.Core.Extensions;
using UmbracoFood.Core.Models;
using UmbracoFood.ViewModels;

namespace UmbracoFood.Mapping
{
    public class StatusMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Status, StatusItem>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => ((OrderStatus) s.Id).GetDescription()));
        }
    }
}