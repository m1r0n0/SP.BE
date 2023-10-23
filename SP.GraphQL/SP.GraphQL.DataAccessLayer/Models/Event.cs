using System.ComponentModel.DataAnnotations;
using HotChocolate.Authorization;

namespace SP.GraphQL.DataAccessLayer.Models;

[Authorize]
public class Event
{
    [Key]
    public int EventId { get; set; }
    public int ServiceId { get; set; }
    public string CustomerUserId { get; set; }
    public string DateOfStart { get; set; }
    public string DateOfEnd { get; set; }
    
}