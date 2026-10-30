using Customers.Application.Customers.DTOs;
using Customers.Application.Customers.Interfaces;
using Customers.Domain.Entities;

namespace Customers.Application.Customers.Services
{
    public class CustomerService(ICustomerRepository repo)
    {
        public async Task<Customer> CreateCustomerAsync(CreateCustomerRequest request)
        {
            var customer = new Customer
            {
                CustomerNumber = request.CustomerNumber,
                CustomerType = request.CustomerType,
                FirstName = request.FirstName,
                LastName = request.LastName,
                CompanyName = request.CompanyName,
                OrganisationNumber = request.OrganisationNumber,
                ContactPerson = request.ContactPerson,
                Address = request.Address,
                PostalCode = request.PostalCode,
                City = request.City,
                Phone = request.Phone,
                Email = request.Email,
                Preferences = request.Preferences
            };

            return await repo.AddAsync(customer);
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
            => await repo.GetByIdAsync(id);
    }
}
