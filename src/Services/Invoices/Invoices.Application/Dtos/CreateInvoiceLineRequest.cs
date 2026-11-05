using Invoices.Domain.Entities;

namespace Invoices.Application.Dtos;

public class CreateInvoiceLineRequest
{
    public required string Description { get; set; }
    public decimal Quantity { get; set; }
    public InvoiceUnit Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal VatRate { get; set; } = 0.25m;
}
