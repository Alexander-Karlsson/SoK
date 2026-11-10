using Customers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Application.Customers.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> AddAsync(Customer customer);
        Task<IReadOnlyCollection<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid id);
        Task<IReadOnlyCollection<Customer>> SearchByNameAsync(string query);
        Task<Customer?> GetByCustomerNumberAsync(string customerNumber);
        Task<Customer> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(Guid id);
    }
}
