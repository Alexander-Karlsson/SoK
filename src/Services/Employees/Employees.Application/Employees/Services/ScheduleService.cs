using Employees.Application.Employees.DTOs.Schedule;
using Employees.Application.Employees.Interfaces;
using Employees.Domain.Entities;

namespace Employees.Application.Employees.Services;

public class ScheduleService(IScheduleRepository repository)
{
    public async Task<ScheduleResponse> CreateScheduleAsync(CreateScheduleRequest request)
    {
        var schedule = new Schedule
        {
            EmployeeId = request.EmployeeId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            BreakMinutes = request.BreakMinutes
        };

        var created = await repository.AddAsync(schedule);
        return ToResponse(created);
    }

    public async Task<ScheduleResponse> GetScheduleByIdAsync(Guid id)
    {
        var schedule = await repository.GetByIdAsync(id);
        return ToResponse(schedule);
    }

    public async Task<List<ScheduleResponse>> GetSchedulesByEmployeeIdAsync(Guid employeeId)
    {
        var schedules = await repository.GetByEmployeeIdAsync(employeeId);
        return schedules.Select(ToResponse).ToList();
    }

    public async Task<ScheduleResponse> UpdateScheduleAsync(Guid id, UpdateScheduleRequest request)
    {
        var schedule = await repository.GetByIdAsync(id);

        schedule.StartTime = request.StartTime;
        schedule.EndTime = request.EndTime;
        schedule.BreakMinutes = request.BreakMinutes;

        var updated = await repository.UpdateAsync(schedule);
        return ToResponse(updated);
    }

    public Task DeleteScheduleAsync(Guid id)
    {
        return repository.DeleteAsync(id);
    }

    private static ScheduleResponse ToResponse(Schedule s) => new()
    {
        Id = s.Id,
        EmployeeId = s.EmployeeId,
        StartTime = s.StartTime,
        EndTime = s.EndTime,
        BreakMinutes = s.BreakMinutes
    };
}