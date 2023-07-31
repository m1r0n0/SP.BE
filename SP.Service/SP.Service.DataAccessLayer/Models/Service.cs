using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SP.Service.DataAccessLayer.Models
{
    [Index("ProviderId")]
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public int Price { get; set; }
        public string ProviderId { get; set; }
        public IList<Event> Events { get; set; } = new List<Event>();
    }
}
