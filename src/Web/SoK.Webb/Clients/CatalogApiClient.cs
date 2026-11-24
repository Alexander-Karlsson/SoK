using System.Text.Json;
using System.Text.Json.Serialization;
using Bookings.Application.Bookings.DTOs.Request;
using Bookings.Application.Bookings.DTOs.Response;
using System.Net;

namespace SoK.Webb.Clients;

public class CatalogApiClient(HttpClient httpClient)
{
    private static readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    // ---------- Maskiner ----------

    public async Task<List<MachineResponse>> GetMachinesAsync()
        => await httpClient.GetFromJsonAsync<List<MachineResponse>>("api/machines", jsonOptions) ?? [];

    public async Task<MachineResponse?> GetMachineAsync(Guid id)
    {
        var response = await httpClient.GetAsync($"api/machines/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<MachineResponse>(jsonOptions);
    }

    public async Task<string?> CreateMachineAsync(CreateMachineRequest request)
        => await ErrorOrNullAsync(await httpClient.PostAsJsonAsync("api/machines", request, jsonOptions));

    public async Task<string?> UpdateMachineAsync(Guid id, UpdateMachineRequest request)
        => await ErrorOrNullAsync(await httpClient.PutAsJsonAsync($"api/machines/{id}", request, jsonOptions));

    public async Task<string?> DeleteMachineAsync(Guid id)
        => await ErrorOrNullAsync(await httpClient.DeleteAsync($"api/machines/{id}"));

    // ---------- Tjänster ----------

    public async Task<List<BusinessServiceResponse>> GetServicesAsync()
        => await httpClient.GetFromJsonAsync<List<BusinessServiceResponse>>("api/businessservices", jsonOptions) ?? [];

    public async Task<BusinessServiceResponse?> GetServiceAsync(Guid id)
    {
        var response = await httpClient.GetAsync($"api/businessservices/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<BusinessServiceResponse>(jsonOptions);
    }

    public async Task<string?> CreateServiceAsync(CreateBusinessServiceRequest request)
        => await ErrorOrNullAsync(await httpClient.PostAsJsonAsync("api/businessservices", request, jsonOptions));

    public async Task<string?> UpdateServiceAsync(Guid id, UpdateBusinessServiceRequest request)
        => await ErrorOrNullAsync(await httpClient.PutAsJsonAsync($"api/businessservices/{id}", request, jsonOptions));

    public async Task<string?> DeleteServiceAsync(Guid id)
        => await ErrorOrNullAsync(await httpClient.DeleteAsync($"api/businessservices/{id}"));

    // ---------- Gemensamt ----------

    // null = lyckades, annars ett meddelande att visa för användaren
    private static async Task<string?> ErrorOrNullAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return null;

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => "Posten finns inte längre.",
            HttpStatusCode.BadRequest or HttpStatusCode.Conflict => await response.Content.ReadAsStringAsync(),
            _ => "Något gick fel, försök igen."
        };
    }

}
