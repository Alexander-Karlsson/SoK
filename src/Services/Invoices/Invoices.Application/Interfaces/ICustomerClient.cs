using Invoices.Application.Dtos;

namespace Invoices.Application.Interfaces;

public interface ICustomerClient
{
    Task<CustomerInfo?> GetByIdAsync(Guid customerId);
}
