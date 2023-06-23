using AutoMapper;
using SP.Identity.BusinessLayer.DTOs;
using SP.Identity.DataAccessLayer.Models;

namespace SP.Identity.API.MappingProfiles
{
    public class AppUserMappingProfile : Profile
    {
        public AppUserMappingProfile()
        {
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom((src => src.Email)))
                .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
            CreateMap<User, UserToGetDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
