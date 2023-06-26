namespace SP.Identity.API.ViewModels
{
    public class UserAuthenticationVM : UserBaseVM
    {
        public bool RememberMe { get; set; } = false;
    }
}
