using Microsoft.AspNetCore.Identity;
using SP.Identity.BusinessLayer.DTOs;

namespace SP.Identity.API.ViewModels
{
    public class LoginBadRequestVM
    {
        public UserLoginDTO Credentials { get; set; } = new();
        public IdentityResultVM Result { get; set; } = new();
    }
}
