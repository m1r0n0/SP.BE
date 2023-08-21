using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SP.Service.BusinessLayer.DTOs;
using SP.Service.BusinessLayer.Exceptions;
using SP.Service.BusinessLayer.Interfaces;
using SP.Service.DataAccessLayer.Data;
using SP.Service.DataAccessLayer.Models;

namespace SP.Service.BusinessLayer.Services
{
    public class EventService : IEventService
    {
        private readonly ServiceContext _context;
        private readonly IMapper _mapper;
        public EventService(ServiceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Event> AddEvent(int serviceId, EventInfoDTO model)
        {
            var service = await _context.Services.FirstOrDefaultAsync(s => s.ServiceId == serviceId);
            
            if (service is null) throw new NotFoundException();
            
            var events = await _context.Events.Where(e => e.ServiceId == serviceId).ToListAsync();
            var newEvent = _mapper.Map<Event>(model);
            events.Add(newEvent);
            service.Events = events;
            
            await _context.SaveChangesAsync();

            return (newEvent);
        }

        public async Task<List<Event>> GetEventsForProvider(string providerUserId)
        {
            var providerServices = await _context.Services.Where(s => s.ProviderUserId == providerUserId).ToListAsync();
            var events = new List<Event>();
            
            foreach (var service in providerServices)
            {
                var serviceEvents = await _context.Events.Where(e => e.ServiceId == service.ServiceId).ToListAsync();
                events.AddRange(serviceEvents);
            }

            if (events.Count == 0) throw new NotFoundException();
            
            return events;
        }

        public async Task<List<Event>> GetEventsForCustomer(string customerUserId)
        {
            var events = await _context.Events.Where(e => e.CustomerUserId == customerUserId).ToListAsync();

            if (events.Count == 0) throw new NotFoundException();
            
            return events;
        }

        public async Task GetUnavailableHours(int serviceId)
        {
            var serviceEvents = await _context.Events.Where(e => e.ServiceId == serviceId).ToListAsync();
            var availabilitySchedules = new List<AvailabilityScheduleDTO>();

            foreach (var serviceEvent in serviceEvents)
            {
                var schedule = new AvailabilityScheduleDTO();
                var date = serviceEvent.DateOfStart;
                DateTime a = new DateTime();
                
            }
        }
    }
}
