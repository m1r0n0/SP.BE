using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SP.Identity.BusinessLayer.DTOs;
using SP.Identity.BusinessLayer.Interfaces;
using SP.Identity.DataAccessLayer.Models;
using SP.Identity.BusinessLayer.Exceptions;
using Microsoft.Extensions.Configuration;

namespace SP.Identity.BusinessLayer.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly DataAccessLayer.Data.IdentityContext _context;
        private readonly IConfiguration _configuration;

        public IdentityService(DataAccessLayer.Data.IdentityContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string CreateToken(User user)
        {
            //create claims details based on the user information
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserId", user.Id),
                new Claim("Email", user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(6),
                signingCredentials: signIn);

            return $"Bearer {new JwtSecurityTokenHandler().WriteToken(token)}"; 
        }

        public async Task<string> GetUserIDFromUserEmail(string userEmail)
        {
            var tempUserEmailToIdDTO = await _context.Users.Where(item => item.Email == userEmail).FirstOrDefaultAsync();

            if (tempUserEmailToIdDTO == null) throw new NotFoundException();

            return tempUserEmailToIdDTO.Id;
        }

        public async Task<bool> CheckGivenEmailForExistingInDB(string email)
        {
            var tempModel = await _context.Users.Where(item => item.Email == email).FirstOrDefaultAsync();

            return tempModel != null;
        }

        public async Task<User> GetUserById(string id)
        {
            User? user = await _context.Users.Where(user => user.Id == id).FirstOrDefaultAsync();

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
