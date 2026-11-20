using System.Net;
using System.Net.Http.Json;
using Bookings.Application.Bookings.DTOs.Invoices;
using Bookings.Application.Bookings.Interfaces;

namespace Bookings.Infrastructure.Clients;

public class InvoiceClient(HttpClient httpClient) : IInvoiceClient
{
    private record CreatedInvoice(Guid Id);

    public async Task<Guid> CreateInvoiceAsync(InvoiceRequest request)
    {
        var response = await httpClient.PostAsJsonAsync("api/invoices", request);

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new ArgumentException(await response.Content.ReadAsStringAsync());

        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<CreatedInvoice>();
        return created!.Id;
    }
}