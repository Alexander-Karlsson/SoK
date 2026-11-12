using Invoices.Domain.Entities;

namespace Invoices.Application.Interfaces;

public interface IInvoiceRepository
{
    Task<Invoice> AddAsync(Invoice invoice, List<InvoiceLine> lines);
    Task<Invoice?> GetByIdAsync(Guid id);
    Task<List<InvoiceLine>> GetLinesAsync(Guid invoiceId);
    Task<List<Invoice>> GetAllAsync();
    Task UpdateAsync(Invoice invoice);
}
