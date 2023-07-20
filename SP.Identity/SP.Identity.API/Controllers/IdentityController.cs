using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SP.Identity.API.ViewModels;
using SP.Identity.BusinessLayer.DTOs;
using SP.Identity.BusinessLayer.Exceptions;
using SP.Identity.BusinessLayer.Interfaces;
using SP.Identity.DataAccessLayer.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SP.Identity.API.Controllers
{
    [ApiController]
    [Route("api/v1/identity/")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<User?> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IIdentityService _accountService;
        private readonly IMapper _mapper;

        public IdentityController(
            UserManager<User?> userManager,
            SignInManager<User> signInManager,
            IIdentityService accountService, 
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(UserAuthenticationVM), 200)]
        [ProducesResponseType(typeof(RegisterBadRequestVM), 400)]
        public async Task<IActionResult> Register(UserRegisterDTO model)
        {
            var user = _mapper.Map<User>(model);
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(_accountService.AssembleRegisterBadRequestVM(model, result));
            
            await _signInManager.SignInAsync(user, false);

            var viewModel = new UserAuthenticationVM(
                model.Email!, 
                _accountService.GetUserIDFromUserEmail(model.Email!).Result,
                true,
                _accountService.CreateToken(user));

            return Ok(viewModel);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(UserAuthenticationVM), 200)]
        [ProducesResponseType(typeof(LoginBadRequestVM), 400)]
        public async Task<IActionResult> Login(UserLoginDTO model)
        {
            if (model.Email is null) return BadRequest(_accountService.AssembleLoginBadRequestVM(model));
            
            var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

            if (!result.Succeeded) return BadRequest(_accountService.AssembleLoginBadRequestVM(model));

            var viewModel = _mapper.Map<UserAuthenticationVM>(model);
            viewModel.UserId = await _accountService.GetUserIDFromUserEmail(model.Email);
            viewModel.Token = _accountService.CreateToken(await _accountService.GetUserById(viewModel.UserId));

            return Ok(viewModel);
        }

        [Authorize]
        [HttpGet]
        [Route("user/get/{userId}")]
        [ProducesResponseType(typeof(UserEmailIdVM), 200)]
        [ProducesResponseType(typeof(UserIdVM), 404)]
        [SwaggerOperation(Summary = "Get user by Id")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            try
            {
                User user = await _accountService.GetUserById(userId);

                return Ok(_mapper.Map<UserEmailIdVM>(user));
            }
            catch (NotFoundException)
            {
                return NotFound(new UserIdVM(userId));
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("user/email/{userId}")]
        [ProducesResponseType(typeof(UserNewEmailDTO),200)]
        [ProducesResponseType(typeof(IdentityResult), 400)]
        [ProducesResponseType( 404)]
        [SwaggerOperation(Summary = "Change user email")]
        public async Task<IActionResult> ChangeUserEmail(string userId, UserNewEmailDTO model)
        {
            try
            {
                User user = await _accountService.GetUserById(userId);
                var token = await _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
                var result = await _userManager.ChangeEmailAsync(user, model.NewEmail, token);

                if (!result.Succeeded) return BadRequest(result);

                await _userManager.SetUserNameAsync(user, model.NewEmail);

                return Ok(model);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("user/password/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IdentityResult), 400)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Change user password")]
        public async Task<IActionResult> ChangeUserPassword(string userId, UserNewPasswordDTO model)
        {
            try
            {
                User user = await _accountService.GetUserById(userId);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (result.Succeeded) return Ok();
                
                return BadRequest(result);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

        }

        [Authorize]
        [HttpDelete]
        [Route("user/delete/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(UserIdVM), 400)]
        [ProducesResponseType(typeof(UserIdVM), 404)]
        [SwaggerOperation(Summary = "Delete user")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                User user = await _accountService.GetUserById(userId);
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded) return Ok();
                
                return BadRequest(new UserIdVM(userId));
            }
            catch (NotFoundException)
            {
                return NotFound(new UserIdVM(userId));
            }
        }
    }
}