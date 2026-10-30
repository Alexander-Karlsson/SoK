using Customers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Application.Customers.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> AddAsync(Customer customer);
    }
}
