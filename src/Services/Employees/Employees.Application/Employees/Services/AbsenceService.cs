using Employees.Application.Employees.DTOs.Absence;
using Employees.Application.Employees.Interfaces;
using Employees.Domain.Entities;
using Employees.Infrastructure;

namespace Employees.Application.Employees.Services;

public class AbsenceService(
    IAbsenceRepository repository,
    IEmployeeRepository employeeRepository)
{
    public async Task<(AbsenceResult Result, AbsenceResponse? Absence)> CreateAbsenceAsync(
        Guid employeeId, CreateAbsenceRequest request)
    {
        if (await employeeRepository.GetByIdAsync(employeeId) is null)
            return (AbsenceResult.EmployeeNotFound, null);

        if (request.EndDate < request.StartDate)
            return (AbsenceResult.InvalidDates, null);

        if (await repository.HasOverlapAsync(employeeId, request.StartDate, request.EndDate))
            return (AbsenceResult.Overlapping, null);

        var absence = new Absence
        {
            EmployeeId = employeeId,
            AbsenceType = request.Type,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Reason = (request.Reason ?? string.Empty).Trim()
        };

        var created = await repository.AddAsync(absence);
        return (AbsenceResult.Ok, ToResponse(created));
    }

    public async Task<AbsenceResponse?> GetAbsenceByIdAsync(Guid employeeId, Guid id)
    {
        var absence = await repository.GetByIdAsync(id);

        if (absence is null || absence.EmployeeId != employeeId)
            return null;

        return ToResponse(absence);
    }

    public async Task<List<AbsenceResponse>> GetAbsencesByEmployeeIdAsync(Guid employeeId)
    {
        var absences = await repository.GetByEmployeeIdAsync(employeeId);
        return absences.Select(ToResponse).ToList();
    }

    public async Task<(AbsenceResult Result, AbsenceResponse? Absence)> UpdateAbsenceAsync(
        Guid employeeId, Guid id, UpdateAbsenceRequest request)
    {
        // Säkerställ att frånvaron finns och tillhör rätt anställd
        var absence = await repository.GetByIdAsync(id);
        if (absence is null || absence.EmployeeId != employeeId)
            return (AbsenceResult.AbsenceNotFound, null);

        if (request.EndDate < request.StartDate)
            return (AbsenceResult.InvalidDates, null);

        // Exkludera nuvarande ID så att frånvaron inte räknas som en krock med sig själv
        if (await repository.HasOverlapAsync(employeeId, request.StartDate, request.EndDate, exceptId: id))
            return (AbsenceResult.Overlapping, null);

        absence.AbsenceType = request.Type;
        absence.StartDate = request.StartDate;
        absence.EndDate = request.EndDate;
        absence.Reason = (request.Reason ?? string.Empty).Trim();

        var updated = await repository.UpdateAsync(absence);
        return (AbsenceResult.Ok, ToResponse(updated));
    }

    public async Task<bool> DeleteAbsenceAsync(Guid employeeId, Guid id)
    {
        var absence = await repository.GetByIdAsync(id);
        if (absence is null || absence.EmployeeId != employeeId)
            return false;

        return await repository.DeleteAsync(id);
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