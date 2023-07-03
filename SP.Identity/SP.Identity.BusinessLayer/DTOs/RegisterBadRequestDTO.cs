using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Identity.BusinessLayer.DTOs
{
    public class RegisterBadRequestDTO
    {
        public string Email { get; set; } = string.Empty;
        public IdentityResultDTO Result { get; set; } = new();

        public RegisterBadRequestDTO(string email, IdentityResultDTO result)
        {
            Email = email;
            Result = result;
        }
        public RegisterBadRequestDTO()
        {
        }
    }
}
