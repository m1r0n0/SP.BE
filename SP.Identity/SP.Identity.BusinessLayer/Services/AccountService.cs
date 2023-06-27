using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SP.Identity.BusinessLayer.DTOs;
using SP.Identity.BusinessLayer.Interfaces;
using SP.Identity.DataAccessLayer.Models;
using SP.Identity.BusinessLayer.Exceptions;

namespace SP.Identity.BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataAccessLayer.Data.IdentityContext _context;
        private readonly IMapper _mapper;
        public AccountService(DataAccessLayer.Data.IdentityContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserEmailIdDTO> GetUserIDFromUserEmail(string userEmail)
        {
            UserEmailIdDTO userEmailIdDTO = new();
            userEmailIdDTO.Email = userEmail;
            var tempUserEmailToIdDTO = await _context.UserList?.Where(item => item.Email == userEmail)?.FirstOrDefaultAsync();
            if (tempUserEmailToIdDTO == null)
            {
                userEmailIdDTO.UserId = "";
            }
            else
            {
                userEmailIdDTO.UserId = tempUserEmailToIdDTO.Id;
            }
            return userEmailIdDTO;
        }

        public async Task<UserEmailIdDTO> GetUserEmailFromUserID(string userId)
        {
            UserEmailIdDTO userEmailIdDTO = new();
            userEmailIdDTO.UserId = userId;
            var tempUserEmailToIdDTO = await _context.UserList?.Where(item => item.Id == userId)?.FirstOrDefaultAsync();
            if (tempUserEmailToIdDTO == null)
            {
                userEmailIdDTO.Email = "";
            }
            else
            {
                userEmailIdDTO.Email = tempUserEmailToIdDTO.Email;
            }
            return userEmailIdDTO;
        }

        public async Task <bool> CheckGivenEmailForExistingInDB(string email)
        {
            var tempModel = await _context.UserList?.Where(item => item.Email == email).FirstOrDefaultAsync();
            if (tempModel == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<UserEmailIdDTO> SetNewUserEmail(string newUserEmail, string userID)
        {
            UserEmailIdDTO userEmailIdDTO = new(userID);
            User? userToPatch = await _context.UserList?.Where(user => user.Id == userID).FirstOrDefaultAsync();
            User? probablyExistingUser = await _context.UserList?.Where(user => user.Email == newUserEmail).FirstOrDefaultAsync();
            if (probablyExistingUser == null)
            {
                UpdateUserInDB();
                userEmailIdDTO.Email = newUserEmail;
            }
            else
            {
                userEmailIdDTO.Email = null;
            }
            return userEmailIdDTO;

            void UpdateUserInDB()
            {
                userToPatch!.Email = newUserEmail;
                userToPatch.NormalizedEmail = newUserEmail.ToUpper();
                userToPatch.UserName = newUserEmail;
                userToPatch.NormalizedUserName = newUserEmail.ToUpper();

                userToPatch = null;

                _context.SaveChanges();
            }
        }

        public async Task<User> GetUserById(string Id)
        {
            User? user = await _context.UserList?.Where(user => user.Id == Id).FirstOrDefaultAsync();
            if (user is null) throw new NotFoundException();
            return user;
        }
    }
}
