using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SP.Identity.API.ViewModels;
using SP.Identity.BusinessLayer.DTOs;
using SP.Identity.BusinessLayer.Interfaces;
using SP.Identity.DataAccessLayer.Models;
using SP.Identity.BusinessLayer.Exceptions;

namespace SP.Identity.BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataAccessLayer.Data.IdentityContext _context;
        public AccountService(DataAccessLayer.Data.IdentityContext context)
        {
            _context = context;
        }
        public async Task<UserEmailIdDTO> GetUserIDFromUserEmail(string userEmail)
        {
            UserEmailIdDTO userEmailIdDTO = new()
            {
                Email = userEmail
            };

            var tempUserEmailToIdDTO = await _context.UserList.Where(item => item.Email == userEmail).FirstOrDefaultAsync();

            if (tempUserEmailToIdDTO == null) throw new NotFoundException();
            
            userEmailIdDTO.UserId = tempUserEmailToIdDTO.Id;

            return userEmailIdDTO;
        }

        public async Task<bool> CheckGivenEmailForExistingInDB(string email)
        {
            var tempModel = await _context.UserList.Where(item => item.Email == email).FirstOrDefaultAsync();

            return tempModel != null;
        }

        public async Task<User> GetUserById(string id)
        {
            User? user = await _context.UserList.Where(user => user.Id == id).FirstOrDefaultAsync();

            return user is null ? throw new NotFoundException() : user;
        }

        public RegisterBadRequestDTO AssembleRegisterBadRequestVM(UserRegisterDTO model, IdentityResult result)
        {
            var errorList = result.Errors.Where(e =>
                    e.Code != nameof(IdentityErrorDescriber.DuplicateUserName) &&
                    e.Code != nameof(IdentityErrorDescriber.InvalidUserName))
                .ToList();

            var badRequestVM = new RegisterBadRequestDTO(model.Email!, new IdentityResultDTO(result.Succeeded, errorList));

            return badRequestVM;
        }

        public LoginBadRequestDTO AssembleLoginBadRequestVM(UserLoginDTO model)
        {
            var errorList = new List<IdentityError>
            {
                new()
                {
                    Code = "InvalidCredentials",
                    Description = "Invalid Email or Password!"
                }
            };

            var badRequestVM = new LoginBadRequestDTO(model.Email!, new IdentityResultDTO(false, errorList));

            return badRequestVM;
        }

    }
}
