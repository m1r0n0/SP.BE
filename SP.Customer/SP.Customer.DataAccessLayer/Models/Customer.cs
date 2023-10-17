using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SP.Customer.DataAccessLayer.Models
{
    [Index("UserId")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string UserId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
