namespace SP.Identity.API.ViewModels
{
    public class UserEmailVM
    {
        public string Email { get; set; } = string.Empty;

        public UserEmailVM(string email)
        {
            Email = email;
        }

        public UserEmailVM()
        {
        }
    }
}
