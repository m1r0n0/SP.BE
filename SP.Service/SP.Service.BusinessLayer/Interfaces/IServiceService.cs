using SP.Service.BusinessLayer.DTOs;
using SP.Service.DataAccessLayer.Models;

namespace SP.Service.BusinessLayer.Interfaces
{
    public interface IServiceService
    {
        Task<DataAccessLayer.Models.Service> CreateService(ServiceInfoDTO model);
        Task<DataAccessLayer.Models.Service> EditService(int serviceId, ServiceNamePriceDTO model); 
        Task<DataAccessLayer.Models.Service> GetService(int serviceId);
        Task DeleteService(int serviceId);
        Task<List<DataAccessLayer.Models.Service>> GetServicesForProvider(string providerUserId);
        Task<List<DataAccessLayer.Models.Service>> GetServices();
        Task DeleteUserInfo(string userId);
    }
}
