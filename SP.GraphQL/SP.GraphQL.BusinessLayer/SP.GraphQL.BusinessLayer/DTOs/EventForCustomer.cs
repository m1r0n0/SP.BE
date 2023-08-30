namespace SP.GraphQL.BusinessLayer.DTOs;

public class EventWithServiceName
{
    public string ServiceName { get; set; }
    public int ServiceId { get; set; }
    public string DateOfStart { get; set; }
    public string DateOfEnd { get; set; }
}