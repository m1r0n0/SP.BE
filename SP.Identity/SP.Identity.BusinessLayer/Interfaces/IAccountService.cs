using SP.Identity.BusinessLayer.DTOs;
using SP.Identity.DataAccessLayer.Models;

namespace SP.Identity.BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        UserEmailIdDTO GetUserIDFromUserEmail(string userEmail);
        UserEmailIdDTO GetUserEmailFromUserID(string userID);
        bool CheckGivenEmailForExistingInDB(string email);
        UserEmailIdDTO SetNewUserEmail(string newUserEmail, string userID);
        Task<User> GetUserById(string Id);
    }
}
