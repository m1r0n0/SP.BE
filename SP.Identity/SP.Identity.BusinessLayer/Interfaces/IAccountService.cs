using Microsoft.AspNetCore.Identity;
using SP.Identity.API.ViewModels;
using SP.Identity.BusinessLayer.DTOs;
using SP.Identity.DataAccessLayer.Models;

namespace SP.Identity.BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        Task<UserEmailIdDTO> GetUserIDFromUserEmail(string userEmail);
        Task<bool> CheckGivenEmailForExistingInDB(string email);
        Task<User> GetUserById(string id);
        RegisterBadRequestDTO AssembleRegisterBadRequestVM(UserRegisterDTO model, IdentityResult result);
        LoginBadRequestDTO AssembleLoginBadRequestVM(UserLoginDTO model);
    }
}
