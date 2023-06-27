using SP.Identity.BusinessLayer.DTOs;
using SP.Identity.DataAccessLayer.Models;

namespace SP.Identity.BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        Task<UserEmailIdDTO> GetUserIDFromUserEmail(string userEmail);
        Task<UserEmailIdDTO> GetUserEmailFromUserID(string userID);
        Task<bool> CheckGivenEmailForExistingInDB(string email);
        Task<UserEmailIdDTO> SetNewUserEmail(string newUserEmail, string userID);
        Task<User> GetUserById(string Id);
    }
}
