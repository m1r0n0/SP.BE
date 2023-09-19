using AutoMapper;
using SP.Customer.BusinessLayer.DTOs;

namespace SP.Customer.API.MappingProfiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<DataAccessLayer.Models.Customer, CustomerDTO>().ReverseMap();
            CreateMap<DataAccessLayer.Models.Customer, CustomerInfoDTO>().ReverseMap();
        }
    }
}
