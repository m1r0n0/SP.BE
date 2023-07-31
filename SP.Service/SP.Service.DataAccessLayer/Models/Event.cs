using System.ComponentModel.DataAnnotations;

namespace SP.Service.DataAccessLayer.Models;

public class Event
{
    [Key]
    public int EventId { get; set; }
    public int ServiceId { get; set; }
    public string CustomerId { get; set; }
    public string DateOfStart { get; set; }
    public string DateOfEnd { get; set; }
    
}