using Customers.Application.Customers.Interfaces;
using Customers.Domain.Entities;
using Customers.Infrastructure.Persistence;


namespace Customers.Infrastructure.Repositories
{
    public class CustomerRepository(CustomersDbContext context) : ICustomerRepository
    {
       
        public async Task<Customer> AddAsync(Customer customer)
        {
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();

            return customer;
        }
    }
}
