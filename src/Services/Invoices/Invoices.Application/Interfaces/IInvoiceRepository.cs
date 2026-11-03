using Invoices.Domain.Entities;

namespace Invoices.Application.Interfaces;

public interface IInvoiceRepository
{
    Task<Invoice> AddAsync(Invoice invoice, List<InvoiceLine> lines);
    Task<Invoice?> GetAsync(Guid id);
    Task<List<InvoiceLine>> GetLinesAsync(Guid invoiceId);
}
