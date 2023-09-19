using SP.GraphQL.BusinessLayer.DTOs;

namespace SP.GraphQL.BusinessLayer.Interfaces;

public interface IGraphQLService
{
    Task<List<ServiceWithProvider>> GetServicesWithProvidersInfo();
    Task<List<EventForCustomer>> GetCustomerEvents(string customerUserId);
    Task<List<EventForProvider>> GetProviderEvents(string providerUserId);
}