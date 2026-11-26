using System.Net;
using Employees.Application.Employees.DTOs;
using Employees.Application.Employees.DTOs.Payslip;

namespace SoK.Webb.Clients;

public class PayslipApiClient(HttpClient httpClient)
{
    public async Task<List<PayslipResponse>> GetAllAsync(Guid employeeId)
        => await httpClient.GetFromJsonAsync<List<PayslipResponse>>(
               $"api/employees/{employeeId}/payslips") ?? new();

    public async Task<(PayslipResponse? Payslip, string? Error)> CreateAsync(
        Guid employeeId, CreatePayslipRequest request)
    {
        var response = await httpClient.PostAsJsonAsync(
            $"api/employees/{employeeId}/payslips", request);

        if (response.IsSuccessStatusCode)
            return (await response.Content.ReadFromJsonAsync<PayslipResponse>(), null);

        if (response.StatusCode is HttpStatusCode.Conflict or HttpStatusCode.BadRequest)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (null, error?.Message ?? "Kunde inte skapa lönespecifikationen.");
        }

        return (null, "Kunde inte skapa lönespecifikationen.");
    }

    public async Task<bool> DeleteAsync(Guid employeeId, Guid id)
    {
        var response = await httpClient.DeleteAsync($"api/employees/{employeeId}/payslips/{id}");
        return response.IsSuccessStatusCode;
    }
}