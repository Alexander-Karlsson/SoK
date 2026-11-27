using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Employees.Application.Employees.DTOs;
using Employees.Application.Employees.DTOs.Absence;

namespace SoK.Webb.Clients;

public class AbsenceApiClient(HttpClient httpClient)
{
    private static readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public async Task<List<AbsenceResponse>> GetAllAsync(Guid employeeId)
        => await httpClient.GetFromJsonAsync<List<AbsenceResponse>>(
               $"api/employees/{employeeId}/absences", jsonOptions) ?? new();

    public async Task<(AbsenceResponse? Absence, string? Error)> CreateAsync(
        Guid employeeId, CreateAbsenceRequest request)
    {
        var response = await httpClient.PostAsJsonAsync(
            $"api/employees/{employeeId}/absences", request, jsonOptions);
        return await HandleResponse(response, "Kunde inte spara frånvaron.");
    }

    public async Task<(AbsenceResponse? Absence, string? Error)> UpdateAsync(
        Guid employeeId, Guid id, UpdateAbsenceRequest request)
    {
        var response = await httpClient.PutAsJsonAsync(
            $"api/employees/{employeeId}/absences/{id}", request, jsonOptions);
        return await HandleResponse(response, "Kunde inte uppdatera frånvaron.");
    }

    public async Task<bool> DeleteAsync(Guid employeeId, Guid id)
    {
        var response = await httpClient.DeleteAsync($"api/employees/{employeeId}/absences/{id}");
        return response.IsSuccessStatusCode;
    }

    private static async Task<(AbsenceResponse?, string?)> HandleResponse(
        HttpResponseMessage response, string defaultError)
    {
        if (response.IsSuccessStatusCode)
            return (await response.Content.ReadFromJsonAsync<AbsenceResponse>(jsonOptions), null);

        if (response.StatusCode is HttpStatusCode.Conflict or HttpStatusCode.BadRequest)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>(jsonOptions);
            return (null, error?.Message ?? defaultError);
        }

        return (null, defaultError);
    }
}