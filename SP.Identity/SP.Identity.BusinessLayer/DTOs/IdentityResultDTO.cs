using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Identity.BusinessLayer.DTOs
{
    public class IdentityResultDTO
    {
        public bool Succeeded { get; set; }
        public List<IdentityError> Errors { get; set; } = new List<IdentityError>();

        public IdentityResultDTO(bool succeeded, List<IdentityError> errors)
        {
            Succeeded = succeeded;
            Errors = errors;
        }
        public IdentityResultDTO()
        {
        }
    }
}
