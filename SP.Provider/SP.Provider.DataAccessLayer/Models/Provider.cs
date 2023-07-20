using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SP.Provider.DataAccessLayer.Models
{
    [Index("UserId")]
    public class Provider
    {
        [Key]
        public int ProviderId { get; set; }
        public string UserId { get; set; } 

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EnterpriseName { get; set; }

        [Required]
        [Range(0, 23)]
        public int WorkHoursBegin { get; set; }

        [Required]
        [Range(0, 23)]
        public int WorkHoursEnd { get; set; }
    }
}
