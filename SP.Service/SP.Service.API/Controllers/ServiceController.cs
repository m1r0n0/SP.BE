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
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;

        public ServiceController(IServiceService serviceService, IMapper mapper)
        {
            _mapper = mapper;
            _serviceService = serviceService;
        }

        [HttpPost]
        [Route("new")]
        [ProducesResponseType(typeof(ServiceDTO), 200)]
        [ProducesResponseType(typeof(ModelErrorVM), 400)]
        [SwaggerOperation(Summary = "Create the Service")]
        public async Task<IActionResult> CreateService(ServiceInfoDTO model)
        {
            try
            {
                var service = await _serviceService.CreateService(model);

                return Ok(_mapper.Map<ServiceDataVM>(service));
            }
            catch (ConflictException)
            {
                return Conflict(model);
            }


        }

        [HttpGet]
        [Route("{serviceId}")]
        [ProducesResponseType(typeof(ServiceInfoDTO), 200)]
        [SwaggerOperation(Summary = "Get the Service")]
        public async Task<IActionResult> GetService(int serviceId)
        {
            try
            {
                var service = await _serviceService.GetService(serviceId);

                return Ok(_mapper.Map<ServiceDataVM>(service));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
        
        [HttpGet]
        [Route("events/provider/{providerUserId}")]
        [ProducesResponseType(typeof(ServiceInfoDTO), 200)]
        [SwaggerOperation(Summary = "Get events for provider")]
        public async Task<IActionResult> GetEventsForProvider(string providerUserId)
        {
            try
            {
                var events = await _serviceService.GetEventsForProvider(providerUserId);
                return Ok(events);
            }
            catch (NotFoundException)
            {
                return NotFound(new List<Event>());
            }
        }
        
        [HttpGet]
        [Route("provider/{providerUserId}")]
        [ProducesResponseType(typeof(ServiceInfoDTO), 200)]
        [SwaggerOperation(Summary = "Get services for provider")]
        public async Task<IActionResult> GetServicesForProvider(string providerUserId)
        {
            try
            {
                var services = await _serviceService.GetServicesForProvider(providerUserId);
                return Ok(_mapper.Map<ServiceDataVM>(services));
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
                var events = await _serviceService.GetEventsForCustomer(customerUserId);
                return Ok(events);
            }
            catch (NotFoundException)
            {
                return NotFound(new List<Event>());
            }
        }


        [HttpPut]
        [Route("{serviceId}")]
        [SwaggerOperation(Summary = "Edit the Service")]
        [ProducesResponseType(typeof(ServiceInfoDTO), 200)]
        [ProducesResponseType(typeof(ModelErrorVM), 400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> EditService(int serviceId, ServiceNamePriceDTO model)
        {
            try
            {
                var service = await _serviceService.EditService(serviceId, model);

                return Ok(_mapper.Map<List<ServiceDataVM>>(service));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("{serviceId}/new/event")]
        [SwaggerOperation(Summary = "Add event to the Service")]
        [ProducesResponseType(typeof(ServiceDTO), 200)]
        public async Task<IActionResult> AddEvent(int serviceId, EventInfoDTO model)
        {
            try
            {
                var service = await _serviceService.AddEvent(serviceId, model);
                
                return Ok(service);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{serviceId}")]
        [SwaggerOperation(Summary = "Delete the Service")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteService(int serviceId)
        {
            try
            {
                await _serviceService.DeleteService(serviceId);

                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

    }
}
