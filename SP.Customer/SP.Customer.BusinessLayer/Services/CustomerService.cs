using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SP.Customer.BusinessLayer.DTOs;
using SP.Customer.BusinessLayer.Exceptions;
using SP.Customer.BusinessLayer.Interfaces;
using SP.Customer.DataAccessLayer.Data;

namespace SP.Customer.BusinessLayer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerContext _context;
        private readonly IMapper _mapper;
        public CustomerService(CustomerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerDTO> CreateCustomer(string userId, CustomerInfoDTO model)
        {
            var isAlreadyExist = await _context.Customers.AnyAsync(p => p.UserId == userId);

            if (isAlreadyExist) throw new ConflictException();

            var customer = _mapper.Map<DataAccessLayer.Models.Customer>(model);
            customer.UserId = userId;

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<CustomerInfoDTO> GetCustomer(string userId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(p => p.UserId == userId);

            if (customer == null) throw new NotFoundException();

            return _mapper.Map<CustomerInfoDTO>(customer);
        }

        public async Task<CustomerInfoDTO> UpdateCustomer(string userId, CustomerInfoDTO model)
        {
            var customer = await _context.Customers.Where(p => p.UserId == userId).FirstOrDefaultAsync();

            if (customer is not null)
            {
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;

                await _context.SaveChangesAsync();
            }
            else throw new NotFoundException();

            return _mapper.Map<CustomerInfoDTO>(customer);
        }

        public async Task DeleteCustomer(string userId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(p => p.UserId == userId);

            if (customer is null) throw new NotFoundException();

            _context.Entry(customer).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }
    }
}
