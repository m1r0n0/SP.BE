using System.ComponentModel.DataAnnotations;

namespace SP.GraphQL.DataAccessLayer.Models
{
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
