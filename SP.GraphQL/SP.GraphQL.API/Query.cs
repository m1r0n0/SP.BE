using SP.GraphQL.BusinessLayer.DTOs;
using SP.GraphQL.BusinessLayer.Interfaces;
using SP.GraphQL.DataAccessLayer.Models;

namespace SP.GraphQL.API.Queries;

public class Query
{
    public async Task<List<ServiceWithProvider>> GetServices(IGraphQLService _graphQLService)
    {
        return await _graphQLService.GetServicesWithProvidersInfo();
    }
}
