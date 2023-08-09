using SP.GraphQL.BusinessLayer.DTOs;

namespace SP.GraphQL.BusinessLayer.Interfaces;

public interface IGraphQLService
{
    Task<List<ServiceWithProvider>> GetServicesWithProvidersInfo();
}