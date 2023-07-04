using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using SP.Provider.API.ViewModels;
using SP.Provider.BusinessLayer.DTOs;
using SP.Provider.BusinessLayer.Exceptions;
using SP.Provider.BusinessLayer.Interfaces;
using SP.Provider.BusinessLayer.Services;

namespace SP.Provider.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataAccessLayer.Models.Provider), 200)]
        [ProducesResponseType(typeof(ModelErrorVM), 400)]
        public async Task<IActionResult> RegisterProvider(CreateProviderDTO model)
        {
            var provider = await _providerService.CreateProvider(model);

            return Ok(provider);
        }

        [HttpPatch]
        [ProducesResponseType(typeof(DataAccessLayer.Models.Provider), 200)]
        [ProducesResponseType(typeof(ModelErrorVM), 400)]
        [ProducesResponseType(typeof(DataAccessLayer.Models.Provider), 404)]
        public async Task<IActionResult> EditProvider(DataAccessLayer.Models.Provider model)
        {
            try
            {
               var provider = await _providerService.UpdateProvider(model);

                return Ok(provider);
            }
            catch (NotFoundException)
            {
                return NotFound(model);
            }
        }

    }
}
