using Customers.Application.Customers.Interfaces;
using Customers.Application.Customers.Services;
using Customers.Infrastructure.Persistence;
using Customers.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Customers.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomersDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("CustomersDb")));

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomerService>();

            return services;
        }
    }
}
