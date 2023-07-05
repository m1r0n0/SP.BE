﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SP.Provider.BusinessLayer.DTOs;
using SP.Provider.BusinessLayer.Exceptions;
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

        public async Task<DataAccessLayer.Models.Provider> CreateProvider(ProviderDTO model)
        {
            var provider = _mapper.Map<DataAccessLayer.Models.Provider>(model);

            await _context.Providers.AddAsync(provider);
            await _context.SaveChangesAsync();

            return provider;
        }

        public async Task<ProviderInfoDTO> UpdateProvider(string userId, ProviderInfoDTO model)
        {
            var provider = await _context.Providers.Where(p => p.UserId == userId).FirstOrDefaultAsync();

            if (provider is not null)
            {
                provider.EnterpriseName = model.EnterpriseName;
                provider.FirstName = model.FirstName;
                provider.LastName = model.LastName;
                provider.WorkHoursBegin = model.WorkHoursBegin;
                provider.WorkHoursEnd = model.WorkHoursEnd;

                await _context.SaveChangesAsync();
            }
            else throw new NotFoundException();

            return _mapper.Map<ProviderInfoDTO>(provider);
        }
    }
}
