using SP.Provider.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Provider.BusinessLayer.Interfaces
{
    public interface IProviderService
    {
        Task<DataAccessLayer.Models.Provider> CreateProvider(ProviderDTO model);
        Task<ProviderInfoDTO> UpdateProvider(string userId, ProviderInfoDTO model);
    }
}
