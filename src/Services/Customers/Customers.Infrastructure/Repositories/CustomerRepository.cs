using Customers.Application.Customers.DTOs;
using Customers.Application.Customers.Interfaces;
using Customers.Domain.Entities;
using Customers.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


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

        public async Task<IReadOnlyCollection<Customer>> GetAllAsync()
            => await context.Customers
                .AsNoTracking()
                .ToListAsync();
        
        public async Task<Customer?> GetByIdAsync(Guid id)
            => await context.Customers.FindAsync(id);

        public async Task<IReadOnlyCollection<Customer>> SearchByNameAsync(string query)
        {
            var customers = await context.Customers
                .AsNoTracking()
                .Where(c => 
                    (c.FirstName != null && c.FirstName.Contains(query)) ||
                    (c.LastName != null && c.LastName.Contains(query)) ||
                    ((c.FirstName ?? "") + " " + (c.LastName ?? "")).Contains(query))
                .ToListAsync();
            
            return customers;
        }

        public async Task<Customer?> GetByCustomerNumberAsync(string customerNumber)
        {
            var customer = await context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerNumber == customerNumber);
            
            return customer ?? null;
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            await context.SaveChangesAsync();

            return customer;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var deleted = await context.Customers.FindAsync(id);
            if (deleted is null)
                return false;
            
            context.Customers.Remove(deleted);
            await context.SaveChangesAsync();
            
            return true;
        }


    }
}
