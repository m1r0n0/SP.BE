using AutoMapper;
using SP.Identity.API.ViewModels;
using SP.Identity.BusinessLayer.DTOs;
using SP.Identity.DataAccessLayer.Models;

namespace SP.Identity.API.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserEmailDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom((src => src.Email)))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .IncludeAllDerived()
                .ReverseMap();

            CreateMap<UserEmailDTO, UserAuthenticationVM>()
                .IncludeAllDerived()
                .ReverseMap();
            CreateMap<UserLoginDTO, UserAuthenticationVM>().ReverseMap();
            CreateMap<UserRegisterDTO, UserAuthenticationVM>().ReverseMap();

            CreateMap<User, UserEmailIdVM>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
