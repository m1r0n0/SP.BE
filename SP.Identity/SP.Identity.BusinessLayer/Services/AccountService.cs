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
           
            if (tempUserEmailToIdDTO == null)
            {
                throw new NotFoundException();
            }

            userEmailIdDTO.UserId = tempUserEmailToIdDTO.Id;

            return userEmailIdDTO;
        }

        public async Task <bool> CheckGivenEmailForExistingInDB(string email)
        {
            var tempModel = await _context.UserList.Where(item => item.Email == email).FirstOrDefaultAsync();

            if (tempModel == null) return false;
            
            return true;
        }

        public async Task<User> GetUserById(string id)
        {
            User? user = await _context.UserList.Where(user => user.Id == id).FirstOrDefaultAsync();

            if (user is null) throw new NotFoundException();

            return user;
        }
    }
}
