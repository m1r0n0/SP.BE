using SP.GraphQL.BusinessLayer.DTOs;
using SP.GraphQL.BusinessLayer.Interfaces;
using SP.GraphQL.DataAccessLayer.Models;

namespace SP.GraphQL.API.Queries;

public class Query
{
    private readonly IGraphQLService _graphQLService;
    Query(IGraphQLService graphQLService)
    {
        _graphQLService = graphQLService;
    }
    public async Task<List<ServiceWithProvider>> GetServices()
    {
        return await _graphQLService.GetServicesWithProvidersInfo() ;
    }
}
