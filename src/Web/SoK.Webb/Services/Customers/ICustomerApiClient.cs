using SoK.Webb.Services.Customers.DTOs;

namespace SoK.Webb.Services.Customers;

public interface ICustomerApiClient
{
    Task<CustomerResponseDto> CreateCustomerAsync(
        CreateCustomerRequest request,
        CancellationToken cancellationToken = default);
}