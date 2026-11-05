using Invoices.Application.Dtos;
using Invoices.Application.Interfaces;
using Invoices.Domain.Entities;

namespace Invoices.Application.Services;

public class InvoiceService(IInvoiceRepository repo)
{
    public async Task<Invoice> CreateInvoiceAsync(CreateInvoiceRequest request)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        var invoiceNumber = DateTime.Now.ToString("yyyyMMddHHmmss");

        var invoice = new Invoice
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            TemplateId = request.TemplateId,
            InvoiceNumber = invoiceNumber,
            OcrReference = invoiceNumber,
            InvoiceDate = today,
            DueDate = today.AddDays(30),
            Status = InvoiceStatus.Draft
        };

        var lines = new List<InvoiceLine>();

        foreach (var lineRequest in request.Lines)
        {
            var line = new InvoiceLine
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoice.Id,
                Description = lineRequest.Description,
                Quantity = lineRequest.Quantity,
                Unit = lineRequest.Unit,
                UnitPrice = lineRequest.UnitPrice,
                VatRate = lineRequest.VatRate,

                Amount = Math.Round(lineRequest.Quantity * lineRequest.UnitPrice, 2)
            };

            lines.Add(line);
        }

        invoice.AmountExclVat = lines.Sum(l => l.Amount);

        invoice.VatAmount = Math.Round(lines.Sum(l => l.Amount * l.VatRate), 2);

        invoice.TotalAmount = invoice.AmountExclVat + invoice.VatAmount;

        return await repo.AddAsync(invoice, lines);
    }

    public async Task<Invoice?> GetByIdAsync(Guid id)
        => await repo.GetByIdAsync(id);
}
