using Customers.Application.Customers.Interfaces;
using Customers.Domain.Entities;

namespace Customers.Application.Tests.Mock;

public class MockCustomerRepository : ICustomerRepository
{
    public Customer? ExistingCustomer { get; set; }
    
    public Task<Customer> AddAsync(Customer customer) 
        => Task.FromResult(customer);

    public Task<IReadOnlyCollection<Customer>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(ExistingCustomer?.Id == id 
            ? ExistingCustomer 
            : null);
    }

    public Task<IReadOnlyCollection<Customer>> SearchByNameAsync(string query)
    {
        throw new NotImplementedException();
    }
    
    public Task<Customer?> GetByCustomerNumberAsync(string customerNumber)
        => Task.FromResult(
            ExistingCustomer?.CustomerNumber == customerNumber
            ? ExistingCustomer
            : null);

    public Task<Customer> UpdateAsync(Customer customer)
    {
        ExistingCustomer = customer;
        return Task.FromResult(customer);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}