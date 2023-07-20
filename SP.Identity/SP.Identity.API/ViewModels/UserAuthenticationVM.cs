namespace SP.Identity.API.ViewModels
{
    public class UserAuthenticationVM : UserEmailIdVM
    {
        public string Token { get; set; } = "Bearer ";
        public bool RememberMe { get; set; } = false;

        public UserAuthenticationVM(string userId, string email, bool rememberMe, string token)
        {
            UserId = userId;
            Email = email;
            RememberMe = rememberMe;
            Token = token;
        }
        public UserAuthenticationVM()
        {
        }
    }
}
