using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Service.API.ViewModels;
using SP.Service.BusinessLayer.DTOs;
using SP.Service.BusinessLayer.Exceptions;
using SP.Service.BusinessLayer.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SP.Service.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/service/")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpPost]
        [Route("new/")]
        [ProducesResponseType(typeof(ServiceDTO), 200)]
        [ProducesResponseType(typeof(ModelErrorVM), 400)]
        [SwaggerOperation(Summary = "Create the Service")]
        public async Task<IActionResult> CreateService(ServiceInfoDTO model)
        {
            try
            {
                var service = await _serviceService.CreateService(model);

                return Ok(service);
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
                var Service = await _serviceService.GetService(serviceId);

                return Ok(Service);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

        }


        [HttpPut]
        [Route("{serviceId}/price/{price}")]
        [SwaggerOperation(Summary = "Change the price of the Service")]
        [ProducesResponseType(typeof(ServiceInfoDTO), 200)]
        [ProducesResponseType(typeof(ModelErrorVM), 400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ChangeServicePrice(int serviceId, int price)
        {
            try
            {
                var service = await _serviceService.ChangePrice(serviceId, price);

                return Ok(service);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
        
        public async Task<IActionResult> AddEvent(){}

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
