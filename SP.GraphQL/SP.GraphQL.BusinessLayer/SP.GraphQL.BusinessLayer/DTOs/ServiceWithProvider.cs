using SP.GraphQL.DataAccessLayer.Models;

namespace SP.GraphQL.BusinessLayer.DTOs;

public class ServiceWithProvider
{
    public int ServiceId { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public Provider Provider { get; set; }
    
}