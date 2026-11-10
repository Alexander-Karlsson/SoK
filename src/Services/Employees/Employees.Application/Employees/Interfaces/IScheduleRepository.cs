using Employees.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Application.Employees.Interfaces;

public interface IScheduleRepository
{
    Task<Schedule> AddAsync(Schedule schedule);
    Task<Schedule> GetByIdAsync(Guid id);
    Task<Schedule> UpdateAsync(Schedule schedule);
    Task<Schedule> DeleteAsync(Guid id);
}
