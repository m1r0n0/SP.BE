namespace SP.GraphQL.BusinessLayer.DTOs;

public class EventForCustomer
{
    public string ServiceName { get; set; }
    public int ServiceId { get; set; }
    public string DateOfStart { get; set; }
    public string DateOfEnd { get; set; }
    public string ProviderName { get; set; }
    public string ProviderEnterpriseName { get; set; }
}