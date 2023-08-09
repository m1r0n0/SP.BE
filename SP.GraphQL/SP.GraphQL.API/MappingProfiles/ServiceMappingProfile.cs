using AutoMapper;
using SP.GraphQL.BusinessLayer.DTOs;

namespace SP.GraphQL.API.MappingProfiles
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<DataAccessLayer.Models.Service, ServiceWithProvider>().ReverseMap();
        }
    }
}
