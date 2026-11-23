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
}

