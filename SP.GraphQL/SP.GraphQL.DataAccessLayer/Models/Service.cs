using System.ComponentModel.DataAnnotations;
using HotChocolate.Authorization;

namespace SP.GraphQL.DataAccessLayer.Models
{
    [Authorize]
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
