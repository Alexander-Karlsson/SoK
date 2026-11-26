using Invoices.Domain.Entities;

namespace Invoices.Application.Dtos;

public class InvoiceResponse
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid TemplateId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public string OcrReference { get; set; } = string.Empty;
    public DateOnly InvoiceDate {  get; set; }
    public DateOnly DueDate { get; set; }
    public decimal AmountExclVat { get; set; }
    public decimal VatAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public InvoiceStatus Status { get; set; }
    public string StatusText { get; set; } = string.Empty;
    public bool CanMarkAsSent { get; set; }
    public bool CanMarkAsPaid { get; set; }


    public List<InvoiceLineResponse> Lines { get; set; } = [];

    public CustomerInfo? Customer {  get; set; }
    public TemplateResponse? Template { get; set; }
}
