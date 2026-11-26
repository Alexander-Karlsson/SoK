using Invoices.Application.Interfaces;
using Invoices.Application.Services;
using Invoices.Infrastructure.Clients;
using Invoices.Infrastructure.Persistence;
using Invoices.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Invoices.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InvoiceDbContext>(options =>
        options.UseSqlServer(
            configuration.GetConnectionString("InvoicesDb")));

        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<InvoiceService>();

        services.AddScoped<ITemplateRepository, TemplateRepository>();
        services.AddScoped<TemplateService>();

        services.AddHttpClient<ICustomerClient, CustomerClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ApiUrls:Customers"]!);
        });

        return services;
    }
}
