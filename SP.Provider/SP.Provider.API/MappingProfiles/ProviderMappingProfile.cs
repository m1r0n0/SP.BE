using AutoMapper;
using SP.Provider.BusinessLayer.DTOs;

namespace SP.Provider.API.MappingProfiles
{
    public class ProviderMappingProfile : Profile
    {
        public ProviderMappingProfile()
        {
            CreateMap<DataAccessLayer.Models.Provider, CreateProviderDTO>().ReverseMap();
        }
    }
}
