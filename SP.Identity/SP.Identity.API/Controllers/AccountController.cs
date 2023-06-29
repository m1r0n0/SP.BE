using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SP.Identity.API.ViewModels;
using SP.Identity.BusinessLayer.DTOs;
using SP.Identity.BusinessLayer.Exceptions;
using SP.Identity.BusinessLayer.Interfaces;
using SP.Identity.DataAccessLayer.Models;
using System.Net;

namespace SP.Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User?> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<User?> userManager,
            SignInManager<User> signInManager,
            IAccountService accountService,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPut]
        [ProducesResponseType(typeof(UserAuthenticationVM), 200)]
        [ProducesResponseType(typeof(RegisterBadRequestVM), 400)]

        public async Task<IActionResult> Register(UserRegisterDTO model)
        {
            var user = _mapper.Map<User>(model);
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errorList = result.Errors.Where(e =>
                        e.Code != nameof(IdentityErrorDescriber.DuplicateUserName) &&
                        e.Code != nameof(IdentityErrorDescriber.InvalidUserName))
                    .ToList();

                var badRequestVM = new RegisterBadRequestVM
                {
                    Credentials = model,
                    Result = { Succeeded = result.Succeeded, Errors = errorList }
                };

                return BadRequest(badRequestVM);
            }

            await _signInManager.SignInAsync(user, false);

            var viewModel = new UserAuthenticationVM
            {
                Email = model.Email!,
                UserId = _accountService.GetUserIDFromUserEmail(model.Email!).Result.UserId,
                RememberMe = true
            };

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserAuthenticationVM), 200)]
        [ProducesResponseType(typeof(LoginBadRequestVM), 400)]
        [ProducesResponseType(typeof(LoginBadRequestVM), 404)]
        public async Task<IActionResult> Login(UserLoginDTO model)
        {
            LoginBadRequestVM badRequestVM;
            if (model.Email is null) return BadRequest(model);

            var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

            if (!result.Succeeded) return BadRequest(model);

            var viewModel = _mapper.Map<UserAuthenticationVM>(model);
            viewModel.UserId = _accountService.GetUserIDFromUserEmail(model.Email).Result.UserId;

            return Ok(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserIdByEmail(string userEmail)
        {
            try
            {
                return Ok(await _accountService.GetUserIDFromUserEmail(userEmail));
            }
            catch (NotFoundException)
            {
                return NotFound(new { Email = userEmail });
            }
        }

        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> ChangeUserEmail(UserEmailIdDTO model)
        {
            try
            {
                User? user = await _accountService.GetUserById(model.UserId);

                if (user is null) throw new NotFoundException();

                var token = await _userManager.GenerateChangeEmailTokenAsync(user, model.Email);
                var result = await _userManager.ChangeEmailAsync(user, model.Email, token);

                if (result.Succeeded)
                {
                    await _userManager.SetUserNameAsync(user, model.Email);
                    return Ok(model);
                }

                return BadRequest(model);
            }
            catch (NotFoundException)
            {
                return NotFound(model);
            }
        }

        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> ChangeUserPassword(UserPasswordIdDTO model)
        {
            try
            {
                User? user = await _accountService.GetUserById(model.UserId);

                if (user is null) throw new NotFoundException();

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (result.Succeeded)
                {
                    return Ok(model);
                }

                return BadRequest(model);
            }
            catch (NotFoundException)
            {
                return NotFound(model);
            }

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                User user = await _accountService.GetUserById(id);

                return Ok(_mapper.Map<UserBaseVM>(user));
            }
            catch (NotFoundException)
            {
                UserAuthenticationVM user = new()
                {
                    UserId = id
                };

                return BadRequest(user);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                User user = await _accountService.GetUserById(userId);
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return Ok(new { Id = userId, result });
                }

                return BadRequest(new { Id = user });
            }
            catch (NotFoundException)
            {
                return NotFound(new { Id = userId });
            }

        }
    }
}