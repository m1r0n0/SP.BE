using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SP.GraphQL.BusinessLayer.DTOs;
using SP.GraphQL.BusinessLayer.Interfaces;
using SP.GraphQL.DataAccessLayer.Data;

namespace SP.GraphQL.BusinessLayer.Services;

public class GraphQLService : IGraphQLService
{
    private readonly GraphQLContext _context;
    private readonly IMapper _mapper;
    
    public GraphQLService(GraphQLContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ServiceWithProvider>> GetServicesWithProvidersInfo()
    {
        var services = await  _context.Services.ToListAsync();
        var servicesWithProviders = new List<ServiceWithProvider>();

        foreach (var service in services)
        {
            ServiceWithProvider serviceWithProvider = new ServiceWithProvider();
            serviceWithProvider.Service = service;
            serviceWithProvider.Provider = await
                _context.Providers.Where(p => p.UserId == service.ProviderUserId).FirstOrDefaultAsync();
            
            servicesWithProviders.Add(serviceWithProvider);
        }

        return servicesWithProviders;
    }
}