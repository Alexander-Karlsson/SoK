using System.Net;
using Employees.Application.Employees.DTOs;
using Employees.Application.Employees.DTOs.EmployeePayroll;

namespace SoK.Webb.Clients;

public class EmployeePayrollApiClient(HttpClient httpClient)
{
    public async Task<EmployeePayrollResponse?> GetAsync(Guid employeeId)
    {
        var response = await httpClient.GetAsync($"api/employees/{employeeId}/payroll");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<EmployeePayrollResponse>();
    }

    public async Task<(EmployeePayrollResponse? Payroll, string? Error)> CreateAsync(
        Guid employeeId, CreateEmployeePayrollRequest request)
    {
        var response = await httpClient.PostAsJsonAsync($"api/employees/{employeeId}/payroll", request);
        return await HandleResponse(response, "Kunde inte spara lönen.");
    }

    public async Task<(EmployeePayrollResponse? Payroll, string? Error)> UpdateAsync(
        Guid employeeId, UpdateEmployeePayrollRequest request)
    {
        var response = await httpClient.PutAsJsonAsync($"api/employees/{employeeId}/payroll", request);
        return await HandleResponse(response, "Kunde inte uppdatera lönen.");
    }

    private static async Task<(EmployeePayrollResponse?, string?)> HandleResponse(
        HttpResponseMessage response, string defaultError)
    {
        if (response.IsSuccessStatusCode)
            return (await response.Content.ReadFromJsonAsync<EmployeePayrollResponse>(), null);

        if (response.StatusCode == HttpStatusCode.Conflict)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (null, error?.Message ?? defaultError);
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
            return (null, "Den anställda hittades inte.");

        return (null, defaultError);
    }
}