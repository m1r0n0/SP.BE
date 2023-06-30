namespace SP.Identity.BusinessLayer.DTOs
{
    public class UserEmailIdDTO : UserEmailDTO
    {
        public string UserId { get; set; } = string.Empty;
        public UserEmailIdDTO(string userId)
        {
            UserId = userId;
        }

        public UserEmailIdDTO() { }
    }
}
