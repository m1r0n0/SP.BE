using SP.Service.BusinessLayer.DTOs;

namespace SP.Service.BusinessLayer.Interfaces
{
    public interface IServiceService
    {
        Task<DataAccessLayer.Models.Service> CreateService(ServiceInfoDTO model);
        Task<DataAccessLayer.Models.Service> ChangePrice(int serviceId, int price);   
        Task<DataAccessLayer.Models.Service> GetService(int serviceId);
        Task DeleteService(int serviceId);
    }
}
