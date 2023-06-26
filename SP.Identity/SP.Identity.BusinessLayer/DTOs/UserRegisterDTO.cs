namespace SP.Identity.BusinessLayer.DTOs
{
    public class UserRegisterDTO : UserBaseDTO
    {
        public string Password { get; set; } = string.Empty;
    }
}
