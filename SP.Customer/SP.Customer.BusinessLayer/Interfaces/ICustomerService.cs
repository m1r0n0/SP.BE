using SP.Customer.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Customer.BusinessLayer.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDTO> CreateCustomer(string userId, CustomerInfoDTO model);
        Task<CustomerInfoDTO> UpdateCustomer(string userId, CustomerInfoDTO model);
        Task<CustomerInfoDTO> GetCustomer(string userId);
        Task DeleteCustomer(string userId);
    }
}
