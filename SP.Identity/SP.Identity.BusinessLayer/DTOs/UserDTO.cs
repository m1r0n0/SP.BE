namespace SP.Identity.BusinessLayer.DTOs
{
    public class UserDTO : UserToGetDTO
    {
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
