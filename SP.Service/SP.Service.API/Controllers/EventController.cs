using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Service.API.ViewModels;
using SP.Service.BusinessLayer.DTOs;
using SP.Service.BusinessLayer.Exceptions;
using SP.Service.BusinessLayer.Interfaces;
using SP.Service.DataAccessLayer.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SP.Service.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/service/")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventController(IEventService eventService, IMapper mapper)
        {
            _mapper = mapper;
            _eventService = eventService;
        }

        [HttpGet]
        [Route("events/provider/{providerUserId}")]
        [ProducesResponseType(typeof(ServiceInfoDTO), 200)]
        [SwaggerOperation(Summary = "Get events for provider")]
        public async Task<IActionResult> GetEventsForProvider(string providerUserId)
        {
            try
            {
                var events = await _eventService.GetEventsForProvider(providerUserId);
                return Ok(events);
            }
            catch (NotFoundException)
            {
                return NotFound(new List<Event>());
            }
        }

        [HttpGet]
        [Route("events/customer/{customerUserId}")]
        [ProducesResponseType(typeof(ServiceInfoDTO), 200)]
        [SwaggerOperation(Summary = "Get events for customer")]
        public async Task<IActionResult> GetEventsForCustomer(string customerUserId)
        {
            try
            {
                var events = await _eventService.GetEventsForCustomer(customerUserId);
                return Ok(events);
            }
            catch (NotFoundException)
            {
                return NotFound(new List<Event>());
            }
        }

        [HttpPost]
        [Route("{serviceId}/new/event")]
        [SwaggerOperation(Summary = "Add event to the Service")]
        [ProducesResponseType(typeof(Event), 200)]
        public async Task<IActionResult> AddEvent(int serviceId, EventInfoDTO model)
        {
            try
            {
                var @event = await _eventService.AddEvent(serviceId, model);
                
                return Ok(@event);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
