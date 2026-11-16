using Employees.Application.Employees.DTOs.Absence;
using Employees.Application.Employees.Interfaces;
using Employees.Domain.Entities;

namespace Employees.Application.Employees.Services;

public class AbsenceService(IAbsenceRepository repository)
{
    public async Task<AbsenceResponse> CreateAbsenceAsync(CreateAbsenceRequest request)
    {
        var absence = new Absence
        {
            EmployeeId = request.EmployeeId,
            AbsenceType = request.Type,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Reason = request.Reason ?? string.Empty
        };

        var created = await repository.AddAsync(absence);
        return ToResponse(created);
    }

    public async Task<AbsenceResponse> GetAbsenceByIdAsync(Guid id)
    {
        var absence = await repository.GetByIdAsync(id);
        return ToResponse(absence);
    }

    public async Task<List<AbsenceResponse>> GetAbsencesByEmployeeIdAsync(Guid employeeId)
    {
        var absences = await repository.GetByEmployeeIdAsync(employeeId);
        return absences.Select(ToResponse).ToList();
    }

    public async Task<AbsenceResponse> UpdateAbsenceAsync(Guid id, UpdateAbsenceRequest request)
    {
        var absence = await repository.GetByIdAsync(id);

        absence.AbsenceType = request.Type;
        absence.StartDate = request.StartDate;
        absence.EndDate = request.EndDate;
        absence.Reason = request.Reason ?? string.Empty;

        var updated = await repository.UpdateAsync(absence);
        return ToResponse(updated);
    }

    public Task DeleteAbsenceAsync(Guid id)
    {
        return repository.DeleteAsync(id);
    }

    private static AbsenceResponse ToResponse(Absence a) => new()
    {
        Id = a.Id,
        EmployeeId = a.EmployeeId,
        Type = a.AbsenceType,
        StartDate = a.StartDate,
        EndDate = a.EndDate,
        Reason = a.Reason
    };
}