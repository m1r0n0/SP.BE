using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Customer.API.ViewModels;
using SP.Customer.BusinessLayer.DTOs;
using SP.Customer.BusinessLayer.Exceptions;
using SP.Customer.BusinessLayer.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SP.Customer.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/customer/")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [Route("new/{userId}")]
        [ProducesResponseType(typeof(CustomerDTO), 200)]
        [ProducesResponseType(typeof(ModelErrorVM), 400)]
        [SwaggerOperation(Summary = "Register the Customer")]
        public async Task<IActionResult> RegisterCustomer(string userId, CustomerInfoDTO model)
        {
            try
            {
                var customer = await _customerService.CreateCustomer(userId, model);

                return Ok(customer);
            }
            catch (ConflictException)
            {
                return Conflict(model);
            }


        }

        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(typeof(CustomerInfoDTO), 200)]
        [SwaggerOperation(Summary = "Get the Customer")]
        public async Task<IActionResult> GetCustomer(string userId)
        {
            try
            {
                var Customer = await _customerService.GetCustomer(userId);

                return Ok(Customer);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

        }


        [HttpPut]
        [Route("{userId}")]
        [SwaggerOperation(Summary = "Edit the Customer")]
        [ProducesResponseType(typeof(CustomerInfoDTO), 200)]
        [ProducesResponseType(typeof(ModelErrorVM), 400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> EditCustomer(string userId, CustomerInfoDTO model)
        {
            try
            {
                var customer = await _customerService.UpdateCustomer(userId, model);

                return Ok(customer);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{userId}")]
        [SwaggerOperation(Summary = "Delete the Customer")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteCustomer(string userId)
        {
            try
            {
                await _customerService.DeleteCustomer(userId);

                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

    }
}
