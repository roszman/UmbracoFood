using AutoMapper;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Infrastructure.Mapping
{
    public class StatusMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<StatusPoco, Status>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name));
        }
    }
}