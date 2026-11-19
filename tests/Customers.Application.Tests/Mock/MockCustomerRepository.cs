using Customers.Application.Customers.Interfaces;
using Customers.Domain.Entities;

namespace Customers.Application.Tests.Mock;

public class MockCustomerRepository : ICustomerRepository
{
    public List<Customer> Customers { get; } = new();
    public Customer? ExistingCustomer { get; set; }
    public List<Customer> SearchResults { get; } = new();

    public Task<Customer> AddAsync(Customer customer) 
        => Task.FromResult(customer);

    public Task<IReadOnlyCollection<Customer>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyCollection<Customer>>(Customers);
    }

    public Task<Customer?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(ExistingCustomer?.Id == id 
            ? ExistingCustomer 
            : null);
    }

    public Task<IReadOnlyCollection<Customer>> SearchByNameAsync(string query)
    {
        return Task.FromResult<IReadOnlyCollection<Customer>>(SearchResults);
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
        var customer = Customers.FirstOrDefault(c => c.Id == id);

        if (customer is null)
            return Task.FromResult(false);

        Customers.Remove(customer);

        return Task.FromResult(true);
    }
}