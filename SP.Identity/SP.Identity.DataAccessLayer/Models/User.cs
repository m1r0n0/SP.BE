using Microsoft.AspNetCore.Identity;

namespace SP.Identity.DataAccessLayer.Models


{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;
        public string DateOfSingUp { get; set; } = string.Empty;


    }
}
