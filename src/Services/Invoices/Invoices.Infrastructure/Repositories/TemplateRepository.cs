using Invoices.Application.Interfaces;
using Invoices.Domain.Entities;
using Invoices.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Infrastructure.Repositories;

public class TemplateRepository(InvoiceDbContext context) : ITemplateRepository
{
    public async Task<InvoiceTemplate> AddAsync(InvoiceTemplate template)
    {
        await context.InvoicesTemplates.AddAsync(template);
        await context.SaveChangesAsync();

        return template;
    }
    public async Task<List<InvoiceTemplate>> GetAllAsync()
        => await context.InvoicesTemplates.ToListAsync();
    public async Task<InvoiceTemplate?> GetByIdAsync(Guid id)
        => await context.InvoicesTemplates.FindAsync(id);
}


