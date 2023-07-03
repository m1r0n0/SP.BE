using AutoMapper;
using SP.Provider.BusinessLayer.DTOs;
using SP.Provider.BusinessLayer.Interfaces;
using SP.Provider.DataAccessLayer.Data;

namespace SP.Provider.BusinessLayer.Services
{
    public class ProviderService : IProviderService
    {
        private readonly ProviderContext _context;
        private readonly IMapper _mapper;
        public ProviderService(ProviderContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DataAccessLayer.Models.Provider> CreateProvider(ProviderCreationDTO model)
        {
            var provider = _mapper.Map<DataAccessLayer.Models.Provider>(model);

            await _context.Providers.AddAsync(provider);
            await _context.SaveChangesAsync();

            return provider;
        } 
    }
}
