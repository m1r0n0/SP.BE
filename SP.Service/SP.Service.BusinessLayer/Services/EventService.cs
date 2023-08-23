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

        public async Task<List<AvailabilityScheduleDTO>> GetUnavailableHours(string providerUserId)
        {
            //Get all provider events
            var providerEvents = await GetEventsForProvider(providerUserId);

            //Get all unavailable schedules
            var unavailabilitySchedules = new List<AvailabilityScheduleDTO>();

            foreach (var serviceEvent in providerEvents)
            {
                var startDate = DateTime.Parse(serviceEvent.DateOfStart, null, System.Globalization.DateTimeStyles.RoundtripKind);
                var endDate = DateTime.Parse(serviceEvent.DateOfEnd, null, System.Globalization.DateTimeStyles.RoundtripKind) ;

                if (DateTime.Compare(startDate, endDate) < 0)
                {
                    if (DateTime.Compare(startDate.Date, endDate.Date) == 0)
                    {
                        unavailabilitySchedules.Add(SetUnavailableHoursForDay(startDate, startDate.Hour, endDate.Hour)); 
                    }
                    else
                    {
                        if (startDate.Year == endDate.Year)
                        {
                            if (startDate.Month == endDate.Month)
                            {
                                unavailabilitySchedules.Add(SetUnavailableHoursForDay(startDate, startDate.Hour, 24));
                                unavailabilitySchedules.AddRange(MakeUnavailableRangeOfDays(startDate.AddDays(1), endDate.Day - 1));
                                unavailabilitySchedules.Add(SetUnavailableHoursForDay(endDate, 0, endDate.Hour));
                            }
                            else
                            {
                                //Make rest of start month unavailable
                                unavailabilitySchedules.Add(SetUnavailableHoursForDay(startDate, startDate.Hour, 24));
                                unavailabilitySchedules.AddRange(
                                    MakeUnavailableRangeOfDays(
                                        startDate.AddDays(1), DateTime.DaysInMonth(startDate.Year, startDate.Month)
                                        )
                                    );
                                
                                //Make months between start & end unavailable
                                for (int i = startDate.Month; i < endDate.Month-1; i++)
                                {
                                    unavailabilitySchedules.AddRange(MakeWholeMonthUnavailable(startDate.Year, i));
                                }
                                
                                //Make end month part unavailable till end date
                                unavailabilitySchedules.AddRange(MakeUnavailableRangeOfDays(new DateTime(endDate.Year, endDate.Month, 1), endDate.Day -1 ));
                                unavailabilitySchedules.Add(SetUnavailableHoursForDay(endDate, 0, endDate.Hour));
                            }
                        }
                        else
                        {
                            //Make rest of start month unavailable
                            unavailabilitySchedules.Add(SetUnavailableHoursForDay(startDate, startDate.Hour, 24));
                            unavailabilitySchedules.AddRange(
                                MakeUnavailableRangeOfDays(
                                    startDate.AddDays(1), DateTime.DaysInMonth(startDate.Year, startDate.Month)
                                )
                            );
                                
                            //Make months between start & end unavailable
                            int passedMonthAmount = (12 - startDate.Month) +
                                                    ((endDate.Year - startDate.Year - 1) * 12) + (endDate.Month - 1);
                            for (int i = startDate.Month; i < passedMonthAmount; i++)
                            {
                                var year = startDate.Year;
                                int yearsPassedAfterStart = Convert.ToInt32((i / 12));
                                
                                if (i % 12 != 0)
                                {
                                    year += yearsPassedAfterStart;
                                }
                                else
                                {
                                    if (i > 12) year += yearsPassedAfterStart - 1;
                                }
                                
                                unavailabilitySchedules.AddRange(MakeWholeMonthUnavailable(year, i));
                            }
                                
                            //Make end month part unavailable till end date
                            unavailabilitySchedules.AddRange(MakeUnavailableRangeOfDays(new DateTime(endDate.Year, endDate.Month, 1), endDate.Day -1 ));
                            unavailabilitySchedules.Add(SetUnavailableHoursForDay(endDate, 0, endDate.Hour));
                        }
                    }
                }
            }

            return unavailabilitySchedules;
        }

        private static AvailabilityScheduleDTO SetUnavailableHoursForDay(DateTime startDate, int startHour, int endHour)
        {
            var schedule = new AvailabilityScheduleDTO(startDate);

            for (int i = startHour; i < endHour; i++)
            {
                schedule.UnavailableHours.Add(i);
            }

            return schedule;
        }

        private static List<AvailabilityScheduleDTO> MakeUnavailableRangeOfDays(DateTime startDate, int endOfRangeMonthDay)
        {
            var schedules = new List<AvailabilityScheduleDTO>();
           

            for (int i = startDate.Day; i <= endOfRangeMonthDay; i++)
            {
                AvailabilityScheduleDTO currentDateSchedule = new AvailabilityScheduleDTO();
                currentDateSchedule.Date = new DateTime(startDate.Year, startDate.Month, i);
                currentDateSchedule = SetUnavailableHoursForDay(currentDateSchedule.Date, 0, 24);
                schedules.Add(currentDateSchedule);
            }

            return schedules;
        }

        private static List<AvailabilityScheduleDTO> MakeWholeMonthUnavailable(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            return MakeUnavailableRangeOfDays(startDate, DateTime.DaysInMonth(year, month));
        }
    }
}
