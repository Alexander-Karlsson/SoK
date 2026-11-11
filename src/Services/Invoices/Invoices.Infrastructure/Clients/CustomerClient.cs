using Invoices.Application.Dtos;
using Invoices.Application.Interfaces;
using System.Net;
using System.Net.Http.Json;

namespace Invoices.Infrastructure.Clients;

public class CustomerClient(HttpClient httpClient) : ICustomerClient
{
    public async Task<CustomerInfo?> GetByIdAsync(Guid customerId)
    {
        var response = await httpClient.GetAsync($"api/customers/{customerId}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<CustomerInfo?>();
    }
}
