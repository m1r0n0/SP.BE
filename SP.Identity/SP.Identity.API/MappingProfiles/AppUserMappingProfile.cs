using AutoMapper;
using SP.Identity.API.ViewModels;
using SP.Identity.BusinessLayer.DTOs;
using SP.Identity.DataAccessLayer.Models;

namespace SP.Identity.API.MappingProfiles
{
    public class AppUserMappingProfile : Profile
    {
        public AppUserMappingProfile()
        {
            CreateMap<UserBaseDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom((src => src.Email)))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .IncludeAllDerived()
                .ReverseMap();

            CreateMap<UserBaseDTO, UserAuthenticationVM>()
                .IncludeAllDerived()
                .ReverseMap();
            CreateMap<UserLoginDTO, UserAuthenticationVM>().ReverseMap();
            CreateMap<UserRegisterDTO, UserAuthenticationVM>().ReverseMap();

            CreateMap<User, UserBaseVM>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
