using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Provider.BusinessLayer.DTOs
{
    public class ProviderCreationDTO
    {
        public string UserId { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string EnterpriseName { get; set; }
        [Required]
        public int WorkHoursBegin { get; set; }
        [Required]
        public int WorkHoursEnd { get; set; }
    }
}
