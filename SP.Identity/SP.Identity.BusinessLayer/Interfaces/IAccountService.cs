using SP.Identity.BusinessLayer.DTOs;
using SP.Identity.DataAccessLayer.Models;

namespace SP.Identity.BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        Task<UserEmailIdDTO> GetUserIDFromUserEmail(string userEmail);
        Task<bool> CheckGivenEmailForExistingInDB(string email);
        Task<User> GetUserById(string id);
    }
}
