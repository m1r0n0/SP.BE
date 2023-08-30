using AutoMapper;
using SP.GraphQL.BusinessLayer.DTOs;
using SP.GraphQL.DataAccessLayer.Models;

namespace SP.GraphQL.API.MappingProfiles
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<DataAccessLayer.Models.Service, ServiceWithProvider>().ReverseMap();
            CreateMap<Event, EventForCustomer>().ReverseMap();
            CreateMap<Event, EventForProvider>().ReverseMap();
        }
    }
}
