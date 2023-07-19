using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using SP.Provider.API.ViewModels;
using SP.Provider.BusinessLayer.DTOs;
using SP.Provider.BusinessLayer.Exceptions;
using SP.Provider.BusinessLayer.Interfaces;
using SP.Provider.BusinessLayer.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SP.Provider.API.Controllers
{
    [ApiController]
    [Route("api/v1/provider/")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpPost]
        [Route("new/{userId}")]
        [ProducesResponseType(typeof(DataAccessLayer.Models.Provider), 200)]
        [ProducesResponseType(typeof(ModelErrorVM), 400)]
        [SwaggerOperation(Summary = "Register the provider")]
        public async Task<IActionResult> RegisterProvider(string userId ,ProviderInfoDTO model)
        {
            try
            {
                var provider = await _providerService.CreateProvider(userId, model);

                return Ok(provider);
            }
            catch (ConflictException)
            {
                return Conflict(model);
            }

            
        }

        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(typeof(ProviderInfoDTO), 200)]
        [SwaggerOperation(Summary = "Get the provider")]
        public async Task<IActionResult> GetProvider(string userId)
        {
            try
            {
                var provider = await _providerService.GetProvider(userId);

                return Ok(provider);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            
        }


        [HttpPut]
        [Route("{userId}")]
        [SwaggerOperation(Summary = "Edit the provider")]
        [ProducesResponseType(typeof(ProviderInfoDTO), 200)]
        [ProducesResponseType(typeof(ModelErrorVM), 400)]
        [ProducesResponseType( 404)]
        public async Task<IActionResult> EditProvider(string userId, ProviderInfoDTO model)
        {
            try
            {
               var provider = await _providerService.UpdateProvider(userId, model);

                return Ok(provider);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

    }
}
