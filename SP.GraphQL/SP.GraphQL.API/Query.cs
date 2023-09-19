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

    public async Task<List<EventForCustomer>> GetCustomerEvents(IGraphQLService _graphQlService,
        string customerUserId)
    {
        return await _graphQlService.GetCustomerEvents(customerUserId);
    }
    
    public async Task<List<EventForProvider>> GetProviderEvents(IGraphQLService _graphQlService,
        string providerUserId)
    {
        return await _graphQlService.GetProviderEvents(providerUserId);
    }
}
