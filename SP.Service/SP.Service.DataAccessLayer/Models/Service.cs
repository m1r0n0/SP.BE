using System.ComponentModel.DataAnnotations;
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
        public IList<Event> Events { get; set; } = new List<Event>();
    }
}
