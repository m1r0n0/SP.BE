using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SP.Service.DataAccessLayer.Models
{
    [Index("ProviderUserId")]
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ProviderUserId { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();
    }
}
