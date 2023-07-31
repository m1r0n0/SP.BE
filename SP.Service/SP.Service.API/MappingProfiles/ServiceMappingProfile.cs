using AutoMapper;
using SP.Service.BusinessLayer.DTOs;
using SP.Service.DataAccessLayer.Models;

namespace SP.Service.API.MappingProfiles
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<DataAccessLayer.Models.Service, ServiceDTO>().ReverseMap();
            CreateMap<Service.DataAccessLayer.Models.Service, ServiceInfoDTO>().ReverseMap();
            CreateMap<EventInfoDTO, Event>().ReverseMap();
        }
    }
}