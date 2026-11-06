using Invoices.Domain.Entities;

namespace Invoices.Application.Dtos;

public class InvoiceLineResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public InvoiceUnit Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal VatRate { get; set; }
    public decimal Amount { get; set; }
}
