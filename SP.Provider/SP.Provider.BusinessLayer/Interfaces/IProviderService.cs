using SP.Provider.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SP.Provider.BusinessLayer.Interfaces
{
    public interface IProviderService
    {
        Task<DataAccessLayer.Models.Provider> CreateProvider(string userId, ProviderInfoDTO model);
        Task<ProviderInfoDTO> GetProvider(string userId);
        Task<ProviderInfoDTO> UpdateProvider(string userId, ProviderInfoDTO model);
        Task DeleteProvider(string userId);
    }
}
