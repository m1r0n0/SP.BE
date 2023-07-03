using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Provider.DataAccessLayer.Models
{
    public class Provider
    {
        public int Id { get; set; }

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
