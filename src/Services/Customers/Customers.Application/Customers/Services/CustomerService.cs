using Customers.Application.Customers.DTOs;
using Customers.Application.Customers.Exceptions;
using Customers.Application.Customers.Interfaces;
using Customers.Application.Customers.Mappers;
using Customers.Application.Customers.Validators;

namespace Customers.Application.Customers.Services
{
    public class CustomerService(ICustomerRepository repo)
    {
        public async Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerRequest request)
        {
            CustomerValidator.ValidateCreate(request);
            
            var existing = await repo.GetByCustomerNumberAsync(request.CustomerNumber);

            if (existing is not null) 
                throw new CustomerAlreadyExistsException(request.CustomerNumber);
            
            var customer = CustomerMapper.ToEntity(request);
            
            var created = await repo.AddAsync(customer);

            return CustomerMapper.ToResponseDto(created);
        }

        public async Task<CustomerResponseDto?> GetByIdAsync(Guid id)
        {
            var customer = await repo.GetByIdAsync(id);

            if (customer is null)
                return null;

            return CustomerMapper.ToResponseDto(customer);
        }
        
        public async Task<IReadOnlyCollection<CustomerResponseDto>> GetAllAsync()
        {
            var customers = await repo.GetAllAsync();

            return customers
                .Select(CustomerMapper.ToResponseDto)
                .ToList();
        }

        public async Task<CustomerResponseDto?> UpdateAsync(Guid id, UpdateCustomerRequest request)
        {
            var existing = await repo.GetByIdAsync(id);

            if (existing == null)
                return null;
            
            CustomerValidator.ValidateUpdate(existing, request);
            
            CustomerMapper.UpdateEntity(existing, request);
            
            var updated = await repo.UpdateAsync(existing);
            
            return CustomerMapper.ToResponseDto(updated);
        }

        public async Task<IReadOnlyCollection<CustomerResponseDto>> SearchByNameAsync(string query)
        {
            var customers = await repo.SearchByNameAsync(query);

            return customers
                .Select(CustomerMapper.ToResponseDto)
                .ToList();
        }
        
        public async Task<CustomerResponseDto?> GetByCustomerNumberAsync(
            string customerNumber)
        {
            var customer = await repo.GetByCustomerNumberAsync(customerNumber);

            if (customer is null)
                return null;

            return CustomerMapper.ToResponseDto(customer);
        }
        
        public Task<bool> DeleteAsync(Guid id)
        {
            return repo.DeleteAsync(id);
        }
    }
}
