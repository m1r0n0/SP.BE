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
        [ProducesResponseType(typeof(ServiceDataVM), 200)]
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
        [Route("provider/{providerUserId}")]
        [ProducesResponseType(typeof(ServiceInfoDTO), 200)]
        [SwaggerOperation(Summary = "Get services for provider")]
        public async Task<IActionResult> GetServicesForProvider(string providerUserId)
        {
            try
            {
                var services = await _serviceService.GetServicesForProvider(providerUserId);
                return Ok(_mapper.Map<List<ServiceDataVM>>(services));
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

                return Ok(_mapper.Map<ServiceDataVM>(service));
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

        [HttpGet]
        [SwaggerOperation(Summary = "Get all services")]
        [ProducesResponseType(typeof(List<ServiceDataVM>), 200)]
        public async Task<IActionResult> GetServices()
        {
            var services = await _serviceService.GetServices();

            return Ok(_mapper.Map<List<ServiceDataVM>>(services));
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete all user-related info")]
        public async Task<IActionResult> DeleteUserInfo(string userId)
        {
            await _serviceService.DeleteUserInfo(userId);
            return Ok();
        }
    }
}
