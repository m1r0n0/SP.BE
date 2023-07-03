namespace SP.Identity.BusinessLayer.DTOs
{
    public class UserRegisterDTO : UserEmailDTO
    {
        public string Password { get; set; } = string.Empty;
    }
}
