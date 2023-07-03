namespace SP.Identity.API.ViewModels
{
    public class UserIdVM
    {
        public string UserId { get; set; } = string.Empty;

        public UserIdVM (string userId)
        {
            UserId = userId;
        }

        public UserIdVM()
        {
        }
    }
}
