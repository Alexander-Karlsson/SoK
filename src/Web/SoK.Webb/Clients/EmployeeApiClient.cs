using Employees.Application.Employees.DTOs.Employee;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SoK.Webb.Clients;

public class EmployeeApiClient(HttpClient httpClient)
{
    private static readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public async Task<List<EmployeeResponse>> GetAllAsync()
        => await httpClient.GetFromJsonAsync<List<EmployeeResponse>>("api/employees", jsonOptions) ?? new();

    public async Task<EmployeeResponse?> GetByIdAsync(Guid id)
    => await httpClient.GetFromJsonAsync<EmployeeResponse>($"api/employees/{id}", jsonOptions);

    public async Task<bool> CreateAsync(CreateEmployeeRequest request)
    {
        var response = await httpClient.PostAsJsonAsync("api/employees", request, jsonOptions);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateEmployeeRequest request)
    {
        var response = await httpClient.PutAsJsonAsync($"api/employees/{id}", request, jsonOptions);
        return response.IsSuccessStatusCode;
    }

    public async Task<EmployeeResponse?> DeactivateAsync(Guid id, DateOnly endDate)
    {
        var request = new DeactivateEmployeeRequest { EmploymentEndDate = endDate };
        var response = await httpClient.PutAsJsonAsync($"api/employees/{id}/deactivate", request, jsonOptions);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<EmployeeResponse>(jsonOptions);
    }

    public async Task<EmployeeResponse?> ReactivateAsync(Guid id, DateOnly startDate)
    {
        var request = new ReactivateEmployeeRequest { EmploymentStartDate = startDate };
        var response = await httpClient.PutAsJsonAsync($"api/employees/{id}/reactivate", request, jsonOptions);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<EmployeeResponse>(jsonOptions);
    }
}