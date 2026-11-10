using Employees.Application.Employees.Interfaces;
using Employees.Domain.Entites;

namespace Employees.Application.Employees.Services;

public class ScheduleService(IScheduleRepository repository)
{
    public async Task<Schedule> CreateScheduleAsync(Schedule schedule)
    {
        return await repository.AddAsync(schedule);
    }

    public async Task<Schedule> GetScheduleByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<Schedule> UpdateScheduleAsync(Schedule schedule)
    {
        return await repository.UpdateAsync(schedule);
    }

    public async Task<Schedule> DeleteScheduleAsync(Guid id)
    {
        return await repository.DeleteAsync(id);
    }
}
