using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SP.Identity.BusinessLayer.DTOs;
using SP.Identity.BusinessLayer.Exceptions;
using SP.Identity.BusinessLayer.Interfaces;
using SP.Identity.DataAccessLayer.Models;

namespace GoalTrackerAPI.Controllers
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
        public async Task<IActionResult> Register(UserDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(model);
            if (_accountService.CheckGivenEmailForExistingInDB(model.Email))
                return Conflict(model);
            var user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(model);
            model.UserId = _accountService.GetUserIDFromUserEmail(model.Email).UserId;
            await _signInManager.SignInAsync(user, false);
            return Ok(model);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody] UserDTO model)
        {
            var result =
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (!result.Succeeded) return BadRequest(model);
            UserEmailIdDTO emailIdDTO = _accountService.GetUserIDFromUserEmail(model.Email);
            model.UserId = emailIdDTO.UserId;
            return Ok(model);
        }

        [HttpGet]
        public UserEmailIdDTO GetUserIdByEmail(string userEmail)
        {
            return _accountService.GetUserIDFromUserEmail(userEmail);
        }

        [HttpGet]
        public UserEmailIdDTO GetUserEmailById(string userId)
        {
            return _accountService.GetUserEmailFromUserID(userId);
        }

        [HttpPatch]
        public UserEmailIdDTO ChangeUserEmail(UserEmailIdDTO model)
        {
            return _accountService.SetNewUserEmail(model.NewEmail!, model.UserId);
        }

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

        [HttpGet]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                User user = await _accountService.GetUserById(id);
                return Ok(_mapper.Map<UserToGetDTO>(user));
            }
            catch (NotFoundException)
            {
                UserToGetDTO user = new()
                {
                    UserId = id
                };
                return BadRequest(user);
            }
            
        }
    }
}