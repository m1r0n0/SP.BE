using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Provider.DataAccessLayer.Attributes;

namespace SP.Provider.DataAccessLayer.Models
{
    public class Provider
    {
        [Key]
        public int ProviderId { get; set; }

        public string UserId { get; set; } 

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EnterpriseName { get; set; }

        [Required]
        [Range(0, 24)]
        public int WorkHoursBegin { get; set; }

        [Required]
        [Range(0, 24)]
        [NotEqual("WorkHoursBegin")]
        public int WorkHoursEnd { get; set; }
    }
}
