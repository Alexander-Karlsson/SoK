using Employees.Infrastructure.Persistence;
using Employees.Infrastructure.Repositories;
using Employees.Application.Employees.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Employees.Application.Employees.Interfaces;

namespace Employees.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EmployeesDbContext>(options =>
        options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IAbsenceRepository, AbsenceRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<IEmployeePayrollRepository, EmployeePayrollRepository>();
        services.AddScoped<IPayslipRepository, PayslipRepository>();
        services.AddScoped<EmployeeService>();
        services.AddScoped<AbsenceService>();
        services.AddScoped<ScheduleService>();
        services.AddScoped<EmployeePayrollService>();
        services.AddScoped<PayslipService>();
        return services;
    }
}
