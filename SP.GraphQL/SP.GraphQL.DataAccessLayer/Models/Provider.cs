using System.ComponentModel.DataAnnotations;
using HotChocolate.Authorization;

namespace SP.GraphQL.DataAccessLayer.Models
{
    [Authorize]
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
