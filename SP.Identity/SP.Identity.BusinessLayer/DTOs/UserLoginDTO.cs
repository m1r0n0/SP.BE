using System.ComponentModel.DataAnnotations;

namespace SP.Identity.BusinessLayer.DTOs
{
    public class UserLoginDTO : UserRegisterDTO
    {
        public bool RememberMe { get; set; }
    }
}
