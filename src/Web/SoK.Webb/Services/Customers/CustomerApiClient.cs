using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using SoK.Webb.Services.Customers.DTOs;

namespace SoK.Webb.Services.Customers;

public class CustomerApiClient(HttpClient httpClient) 
    : ICustomerApiClient
{
    private static readonly JsonSerializerOptions JsonOptions = 
        new(JsonSerializerDefaults.Web)
        {
            Converters = { new JsonStringEnumConverter() }
        };

    public async Task<CustomerResponseDto> CreateCustomerAsync(
        CreateCustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PostAsJsonAsync(
            "api/customers",
            request,
            JsonOptions,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content
                   .ReadFromJsonAsync<CustomerResponseDto>(
                       JsonOptions,
                       cancellationToken)
               ?? throw new InvalidOperationException(
                   "Customers API returned empty response.");
    }
}