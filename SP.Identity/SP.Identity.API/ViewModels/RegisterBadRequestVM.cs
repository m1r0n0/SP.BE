using Microsoft.AspNetCore.Identity;
using SP.Identity.BusinessLayer.DTOs;

namespace SP.Identity.API.ViewModels
{
    public class RegisterBadRequestVM
    {
        public UserRegisterDTO Credentials { get; set; } = new();
        public IdentityResultVM Result { get; set; } = new();
    }
}
