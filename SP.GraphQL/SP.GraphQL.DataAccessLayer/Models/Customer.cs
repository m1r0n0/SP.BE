using System.ComponentModel.DataAnnotations;
using HotChocolate.Authorization;

namespace SP.GraphQL.DataAccessLayer.Models
{
    [Authorize]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string UserId { get; set; } 

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
