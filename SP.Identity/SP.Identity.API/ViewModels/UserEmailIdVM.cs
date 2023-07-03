namespace SP.Identity.API.ViewModels
{
    public class UserEmailIdVM : UserIdVM
    {
        public string Email { get; set; } = string.Empty;

        public UserEmailIdVM(string userId, string email)
        {
            UserId = userId;
            Email = email;
        }

        public UserEmailIdVM()
        {
        }
    }
}
