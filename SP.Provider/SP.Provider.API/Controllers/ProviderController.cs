using Microsoft.AspNetCore.Mvc;
using SP.Provider.BusinessLayer.DTOs;
using SP.Provider.BusinessLayer.Interfaces;
using SP.Provider.BusinessLayer.Services;

namespace SP.Provider.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController (IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpPut]
        [ProducesResponseType(typeof(DataAccessLayer.Models.Provider), 200)]
        public async Task<IActionResult> RegisterProvider(ProviderCreationDTO model)
        {
            var provider = await _providerService.CreateProvider(model);

            return Ok(provider);
        }

    }
}
