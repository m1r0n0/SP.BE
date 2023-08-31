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

    public async Task<List<EventForCustomer>> GetCustomerEvents(string customerUserId)
    {
        var events = await _context.Events.Where(e => e.CustomerUserId == customerUserId).ToListAsync();
        var customerEvents = new List<EventForCustomer>();

        foreach (var @event in events)
        {
            var service = await _context.Services.FirstOrDefaultAsync(s => s.ServiceId == @event.ServiceId);
            var eventForCustomer = _mapper.Map<EventForCustomer>(@event);
            eventForCustomer.ServiceName = service.Name;

            var provider = await _context.Providers.FirstOrDefaultAsync(p => p.UserId == service.ProviderUserId);
            eventForCustomer.ProviderName = $"{provider.FirstName} {provider.LastName}";
            eventForCustomer.ProviderEnterpriseName = provider.EnterpriseName;
            
            customerEvents.Add(eventForCustomer);
        }
        return customerEvents;
    }
    
    public async Task<List<EventForProvider>> GetProviderEvents(string providerUserId)
    {
        var customerEvents = new List<EventForProvider>();
        var providerServices = await _context.Services.Where(s => s.ProviderUserId == providerUserId).ToListAsync();

        foreach (var service in providerServices)
        {
            var events = await _context.Events.Where(e => e.ServiceId == service.ServiceId).ToListAsync();
            foreach (var @event in events)
            {
                var eventForProvider = _mapper.Map<EventForProvider>(@event);
                eventForProvider.ServiceName = service.Name;
                
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == @event.CustomerUserId);
                eventForProvider.CustomerName = string.Format("{0} {1}", customer.FirstName, customer.LastName);
                
                customerEvents.Add(eventForProvider);
            }
        }
        
        return customerEvents;
    }
}