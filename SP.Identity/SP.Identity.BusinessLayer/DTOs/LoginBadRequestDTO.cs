using Microsoft.AspNetCore.Identity;
using SP.Identity.BusinessLayer.DTOs;

namespace SP.Identity.API.ViewModels
{
    public class LoginBadRequestDTO
    {
        public string Email { get; set; } = string.Empty;
        public IdentityResultDTO Result { get; set; } = new();
        public LoginBadRequestDTO(string email, IdentityResultDTO result)
        {
            Email = email;
            Result = result;
        }
        public LoginBadRequestDTO()
        {
        }
    }
}
