using SP.Service.BusinessLayer.DTOs;
using SP.Service.DataAccessLayer.Models;

namespace SP.Service.BusinessLayer.Interfaces
{
    public interface IEventService
    {
        Task<Event> AddEvent(int serviceId, EventInfoDTO model);
        Task<List<Event>> GetEventsForProvider(string providerUserId);
        Task<List<Event>> GetEventsForCustomer(string customerUserId);
        Task<List<AvailabilityScheduleDTO>> GetUnavailableHours(string providerUserId);
    }
}
