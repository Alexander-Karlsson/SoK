using Invoices.Application.Interfaces;
using Invoices.Domain.Entities;
using Invoices.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Infrastructure.Repositories;

public class InvoiceRepository(InvoiceDbContext context) : IInvoiceRepository
{
    public async Task<Invoice> AddAsync(Invoice invoice, List<InvoiceLine> lines)
    {
        await context.Invoices.AddAsync(invoice);

        foreach (var line in lines)
        {
            await context.InvoicesLines.AddAsync(line);
        }

        await context.SaveChangesAsync();
        return invoice;
    }

    public async Task<Invoice?> GetAsync(Guid id)
        => await context.Invoices.FindAsync(id);

    public async Task<List<InvoiceLine>> GetLinesAsync(Guid invoiceId)
        => await context.InvoicesLines
        .Where(l => l.InvoiceId == invoiceId)
        .ToListAsync();
}
