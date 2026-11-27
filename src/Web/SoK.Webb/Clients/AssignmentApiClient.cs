using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;
using Bookings.Application.Bookings.DTOs.Request;
using Bookings.Application.Bookings.DTOs.Response;

namespace SoK.Webb.Clients;

public class AssignmentApiClient(HttpClient httpClient)
{
    private static readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    // Lediga uppdrag som en anställd med titeln får ta. Utan titel: bara uppdrag utan titelkrav.
    public async Task<List<OpenAssignmentResponse>> GetOpenAssignmentsAsync(string? title)
    {
        var url = string.IsNullOrWhiteSpace(title)
            ? "api/bookings/open"
            : $"api/bookings/open?title={Uri.EscapeDataString(title)}";

        return await httpClient.GetFromJsonAsync<List<OpenAssignmentResponse>>(url, jsonOptions) ?? [];
    }

    public async Task<string?> TakeServiceAsync(Guid bookingId, Guid serviceId, Guid employeeId)
    {
        var response = await httpClient.PostAsJsonAsync(
            $"api/bookings/{bookingId}/services/{serviceId}/take",
            new TakeServiceRequest { EmployeeId = employeeId },
            jsonOptions);

        if (response.IsSuccessStatusCode)
            return null;

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => "Uppdraget finns inte längre.",
            HttpStatusCode.BadRequest or HttpStatusCode.Conflict
                => await response.Content.ReadAsStringAsync(),
            _ => "Något gick fel, försök igen."
        };
    }

    public async Task<string?> ReleaseServiceAsync(Guid bookingId, Guid serviceId, Guid employeeId)
    {
        var response = await httpClient.PostAsJsonAsync(
            $"api/bookings/{bookingId}/services/{serviceId}/release",
            new ReleaseServiceRequest { EmployeeId = employeeId },
            jsonOptions);

        if (response.IsSuccessStatusCode)
            return null;

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => "Uppdraget finns inte längre.",
            HttpStatusCode.BadRequest or HttpStatusCode.Conflict
                => await response.Content.ReadAsStringAsync(),
            _ => "Något gick fel, försök igen."
        };
    }

    public async Task<List<MyAssignmentResponse>> GetAssignmentsAsync(Guid employeeId)
        => await httpClient.GetFromJsonAsync<List<MyAssignmentResponse>>(
            $"api/bookings/assignments?employeeId={employeeId}", jsonOptions) ?? [];

    public async Task<string?> ReportTimeAsync(Guid bookingId, Guid serviceId, ReportTimeRequest request)
    {
        var response = await httpClient.PostAsJsonAsync(
            $"api/bookings/{bookingId}/services/{serviceId}/time", request, jsonOptions);

        if (response.IsSuccessStatusCode)
            return null;

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => "Uppdraget finns inte längre.",
            HttpStatusCode.BadRequest or HttpStatusCode.Conflict
                => await response.Content.ReadAsStringAsync(),
            _ => "Något gick fel, försök igen."
        };
    }

    // null om bokningen inte finns
    public async Task<List<ServiceTimeResponse>?> GetReportedTimeAsync(Guid bookingId)
    {
        var response = await httpClient.GetAsync($"api/bookings/{bookingId}/time");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ServiceTimeResponse>>(jsonOptions);
    }

}
