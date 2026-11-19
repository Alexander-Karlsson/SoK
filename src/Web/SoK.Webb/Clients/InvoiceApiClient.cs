using Invoices.Application.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SoK.Webb.Clients;

public class InvoiceApiClient(HttpClient httpClient)
{
    // Status kommer som text istället för siffra, tex Draft
    private static readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    // Hämtar alla fakturor, ger en tom lista om det är tomt
    public async Task<List<InvoiceResponse>> GetAllAsync()
    {
        var invoices = await httpClient.GetFromJsonAsync<List<InvoiceResponse>>("api/invoices", jsonOptions);

        return invoices ?? new();
    }
}
