﻿using AutoMapper;
using SP.Provider.BusinessLayer.DTOs;

namespace SP.Provider.API.MappingProfiles
{
    public class ProviderMappingProfile : Profile
    {
        public ProviderMappingProfile()
        {
            CreateMap<DataAccessLayer.Models.Provider, ProviderDTO>().ReverseMap();
            CreateMap<DataAccessLayer.Models.Provider, ProviderInfoDTO>().ReverseMap();
        }
    }
}
