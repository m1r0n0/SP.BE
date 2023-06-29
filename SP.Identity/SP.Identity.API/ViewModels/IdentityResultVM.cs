using Microsoft.AspNetCore.Identity;

namespace SP.Identity.API.ViewModels
{
    public class IdentityResultVM
    {
        public bool Succeeded { get; set; }
        public List<IdentityError> Errors { get; set; } = new List<IdentityError>();
    }
}
