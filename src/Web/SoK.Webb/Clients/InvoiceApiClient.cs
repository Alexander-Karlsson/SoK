using Invoices.Application.Dtos;
using System.Net;
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

    // Hämtar en faktura med rader, kund och mall, ger null om den inte finns
    public async Task<InvoiceResponse?> GetByIdAsync(Guid id)
    {
        var response = await httpClient.GetAsync($"api/invoices/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InvoiceResponse>(jsonOptions);
    }

    // Hämtar alla fakturamallar, ger en tom lista om det inte finns några
    public async Task<List<TemplateResponse>> GetTemplatesAsync()
    {
        var templates = await httpClient.GetFromJsonAsync<List<TemplateResponse>>("api/templates", jsonOptions);

        return templates ?? new();
    }

    // Hämtar en mall, ger null om den inte finns
    public async Task<TemplateResponse?> GetTemplateByIdAsync(Guid id)
    {
        var response = await httpClient.GetAsync($"api/templates/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TemplateResponse>(jsonOptions);
    }

    // Skapar en ny mall, ger true om det gick bra
    public async Task<bool> CreateTemplateAsync(CreateTemplateRequest request)
    {
        var response = await httpClient.PostAsJsonAsync("api/templates", request);

        return response.IsSuccessStatusCode;
    }

    // Ändrar en mall, ger true om det gick bra
    public async Task<bool> UpdateTemplateAsync(Guid id, CreateTemplateRequest request)
    {
        var response = await httpClient.PutAsJsonAsync($"api/templates/{id}", request);

        return response.IsSuccessStatusCode;
    }

    // Ändrar status på en faktura tex Sent eller Paid, ger true om det gick bra
    public async Task<bool> UpdateStatusAsync(Guid id, string status)
    {
        var response = await httpClient.PutAsJsonAsync($"api/invoices/{id}/status", new { status });

        return response.IsSuccessStatusCode;
    }
}
