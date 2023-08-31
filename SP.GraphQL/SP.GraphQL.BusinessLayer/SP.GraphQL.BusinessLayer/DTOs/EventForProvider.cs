namespace SP.GraphQL.BusinessLayer.DTOs;

public class EventForProvider
{
    public string ServiceName { get; set; }
    public int ServiceId { get; set; }
    public string DateOfStart { get; set; }
    public string DateOfEnd { get; set; }
    public string CustomerName { get; set; }
}