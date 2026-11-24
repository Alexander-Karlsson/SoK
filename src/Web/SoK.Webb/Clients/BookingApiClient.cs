using Bookings.Application.Bookings.DTOs.Request;
using Bookings.Application.Bookings.DTOs.Response;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SoK.Webb.Clients;

public class BookingApiClient(HttpClient httpClient)
{
    private static readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public async Task<List<BookingSummaryResponse>> GetAllAsync()
    {
        var bookings = await httpClient.GetFromJsonAsync<List<BookingSummaryResponse>>("api/bookings", jsonOptions);
        return bookings ?? [];
    }

    public async Task<BookingResponse?> GetByIdAsync(Guid id)
    {
        var response = await httpClient.GetAsync($"api/bookings/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<BookingResponse>(jsonOptions);
    }

    public async Task<string?> CancelAsync(Guid id)
    {
        var response = await httpClient.PostAsync($"api/bookings/{id}/cancel", null);

        if (response.IsSuccessStatusCode)
            return null;

        if (response.StatusCode == HttpStatusCode.NotFound)
            return "Bokningen finns inte.";

        if (response.StatusCode == HttpStatusCode.Conflict)
            return await response.Content.ReadAsStringAsync();

        return "Något gick fel, försök igen.";
    }

    public async Task<string?> CompleteAsync(Guid id, Guid templateId)
    {
        var response = await httpClient.PostAsJsonAsync($"api/bookings/{id}/complete", new { TemplateId = templateId });

        if (response.IsSuccessStatusCode)
            return null;

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => "Bokningen finns inte.",
            HttpStatusCode.ServiceUnavailable => "Faktureringen är inte tillgänglig just nu. Försök igen senare.",
            HttpStatusCode.BadRequest
            or HttpStatusCode.Conflict
                => await response.Content.ReadAsStringAsync(),
            _ => "Något gick fel, försök igen."
        };
    }
    public async Task<List<MachineResponse>> GetMachinesAsync()
    => await httpClient.GetFromJsonAsync<List<MachineResponse>>("api/machines", jsonOptions) ?? [];

    public async Task<List<BusinessServiceResponse>> GetServicesAsync()
        => await httpClient.GetFromJsonAsync<List<BusinessServiceResponse>>("api/businessservices", jsonOptions) ?? [];

    public async Task<(Guid? Id, string? Error)> CreateAsync(CreateBookingRequest request)
    {
        var response = await httpClient.PostAsJsonAsync("api/bookings", request, jsonOptions);

        if (response.IsSuccessStatusCode)
        {
            var created = await response.Content.ReadFromJsonAsync<BookingResponse>(jsonOptions);
            return (created!.Id, null);
        }

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest or HttpStatusCode.Conflict
                => (null, await response.Content.ReadAsStringAsync()),
            _ => (null, "Något gick fel, försök igen.")
        };
    }
}

