using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SP.Service.BusinessLayer.DTOs;
using SP.Service.BusinessLayer.Exceptions;
using SP.Service.BusinessLayer.Interfaces;
using SP.Service.DataAccessLayer.Data;
using SP.Service.DataAccessLayer.Models;

namespace SP.Service.BusinessLayer.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ServiceContext _context;
        private readonly IMapper _mapper;
        public ServiceService(ServiceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DataAccessLayer.Models.Service> CreateService(ServiceInfoDTO model)
        {
            var service = _mapper.Map<DataAccessLayer.Models.Service>(model);

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();

            return service;
        }

        public async Task<DataAccessLayer.Models.Service> GetService(int serviceId)
        {
            var service = await _context.Services.FirstOrDefaultAsync(p => p.ServiceId == serviceId);
            
            if (service == null) throw new NotFoundException();

            return service;
        }

        public async Task<DataAccessLayer.Models.Service> EditService(int serviceId, ServiceNamePriceDTO model)   
        {
            var service = await _context.Services.Where(p => p.ServiceId == serviceId).FirstOrDefaultAsync();

            if (service is null) throw new NotFoundException();

            service.Name = model.Name;
            service.Price = model.Price;

            await _context.SaveChangesAsync();

            return await GetService(serviceId);
        }

        public async Task DeleteService(int serviceId)
        {
            var service = await _context.Services.FirstOrDefaultAsync(p => p.ServiceId == serviceId);

            if (service is null) throw new NotFoundException();

            _context.Entry(service).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<List<DataAccessLayer.Models.Service>> GetServicesForProvider(string providerUserId)
        {
            var services = await _context.Services.Where(e => e.ProviderUserId == providerUserId).ToListAsync();

            if (services.Count == 0) throw new NotFoundException();
            
            return services;
        }

        public async Task<List<DataAccessLayer.Models.Service>> GetServices()
        {
            return await _context.Services.ToListAsync();
        }
    }
}
