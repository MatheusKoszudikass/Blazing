using AutoMapper;
using Blazing.Identity.Dto;
using Blazing.Identity.Entities;

namespace Blazing.Identity.Mappings
{
    public class IdentityMapper : Profile
    {
        public IdentityMapper()
        {
            CreateMap<ApplicationRole, ApplicationRoleDto>().ReverseMap();
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                //.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                //.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
