namespace SP.Identity.API.ViewModels
{
    public class UserAuthenticationVM : UserEmailIdVM
    {
        public bool RememberMe { get; set; } = false;

        public UserAuthenticationVM(string userId, string email, bool rememberMe)
        {
            UserId = userId;
            Email = email;
            RememberMe = rememberMe;
        }
        public UserAuthenticationVM()
        {
        }
    }
}
