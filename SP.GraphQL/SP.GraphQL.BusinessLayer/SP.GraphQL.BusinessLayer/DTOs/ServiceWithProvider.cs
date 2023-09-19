using SP.GraphQL.DataAccessLayer.Models;

namespace SP.GraphQL.BusinessLayer.DTOs;

public class ServiceWithProvider
{
    public Service Service { get; set; }
    public Provider Provider { get; set; }
    public IList<int> AvailableHours = new List<int>();

}