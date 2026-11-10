using Employees.Application.Employees.Interfaces;
using Employees.Domain.Entites;
using Employees.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Infrastructure.Repositories;

public class ScheduleRepository(EmployeesDbContext context) : IScheduleRepository
{
    public async Task<Schedule> AddAsync(Schedule schedule)
    {
        context.Schedules.Add(schedule);
        await context.SaveChangesAsync();
        return schedule;
    }

    public async Task<Schedule> DeleteAsync(Guid id)
    {
        var schedule = await context.Schedules.FindAsync(id);
        if (schedule == null) throw new InvalidOperationException("Schedule not found");

        context.Schedules.Remove(schedule);
        await context.SaveChangesAsync();
        return schedule;
    }

    public async Task<Schedule> GetByIdAsync(Guid id)
    {
        var schedule = await context.Schedules.FindAsync(id);
        return schedule ?? throw new InvalidOperationException("Schedule not found");
    }

    public async Task<Schedule> UpdateAsync(Schedule schedule)
    {
        context.Schedules.Update(schedule);
        await context.SaveChangesAsync();
        return schedule;
    }
}
