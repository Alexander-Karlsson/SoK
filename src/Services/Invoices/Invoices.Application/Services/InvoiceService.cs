using Invoices.Application.Dtos;
using Invoices.Application.Helpers;
using Invoices.Application.Interfaces;
using Invoices.Domain.Entities;

namespace Invoices.Application.Services;

public class InvoiceService(IInvoiceRepository repo, ITemplateRepository templateRepo, ICustomerClient customerClient)
{
    private const decimal CompanyVatRate = 0m;
    private const decimal DefaultVatRate = 0.25m;
    public async Task<InvoiceResponse> CreateInvoiceAsync(CreateInvoiceRequest request)
    {
        if (request.CustomerId == Guid.Empty)
        {
            throw new ArgumentException("En kund måste väljas");
        }

        if (request.TemplateId == Guid.Empty)
        {
            throw new ArgumentException("En fakturamall måste väljas");
        }

        var template = await templateRepo.GetByIdAsync(request.TemplateId);

        if (template == null)
        {
            throw new ArgumentException("Fakturamallen finns inte");
        }

        var customer = await customerClient.GetByIdAsync(request.CustomerId);
        if (customer == null)
        {
            throw new ArgumentException("Kunden finns inte");
        }

        var isCompany = !string.IsNullOrWhiteSpace(customer.OrganisationNumber);
        var vatRate = isCompany ? CompanyVatRate : DefaultVatRate;

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

        var lineNumber = 1;

        foreach (var lineRequest in request.Lines)
        {
            var line = new InvoiceLine
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoice.Id,
                LineNumber = lineNumber,
                Description = lineRequest.Description,
                Quantity = lineRequest.Quantity,
                Unit = lineRequest.Unit,
                UnitPrice = lineRequest.UnitPrice,
                VatRate = vatRate,

                Amount = Math.Round(lineRequest.Quantity * lineRequest.UnitPrice, 2)
            };

            lines.Add(line);

            lineNumber++;
        }

        invoice.AmountExclVat = lines.Sum(l => l.Amount);

        invoice.VatAmount = Math.Round(lines.Sum(l => l.Amount * l.VatRate), 2);

        invoice.TotalAmount = invoice.AmountExclVat + invoice.VatAmount;

        await repo.AddAsync(invoice, lines);

        return ToResponse(invoice, lines);
    }

    public async Task<InvoiceResponse?> UpdateStatusAsync(Guid id, UpdateInvoiceStatusRequest request)
    {
        var invoice = await repo.GetByIdAsync(id);

        if (invoice == null)
        {
            return null;
        }

        if (!IsAllowedStatusChange(invoice.Status, request.Status))
        {
            throw new ArgumentException(
                 $"Går inte att ändra från {InvoiceText.Status(invoice.Status)} till {InvoiceText.Status(request.Status)}");
        }

        invoice.Status = request.Status;

        await repo.UpdateAsync(invoice);

        var lines = await repo.GetLinesAsync(id);
        return ToResponse(invoice, lines);
    }
    public async Task<InvoiceResponse?> GetByIdAsync(Guid id)
    {
        var invoice = await repo.GetByIdAsync(id);

        if (invoice == null)
        {
            return null;
        }

        var lines = await repo.GetLinesAsync(id);
        var response = ToResponse(invoice, lines);

        response.Customer = await customerClient.GetByIdAsync(invoice.CustomerId);

        var template = await templateRepo.GetByIdAsync(invoice.TemplateId);

        response.Template = template == null ? null : TemplateService.ToResponse(template);

        return response;
    }

    public async Task<List<InvoiceResponse>> GetAllAsync()
    {
        var invoices = await repo.GetAllAsync();

        var responses = invoices.Select(i => ToResponse(i, [])).ToList();

        var customerIds = responses.Select(r => r.CustomerId).Distinct();

        var customers = new Dictionary<Guid, CustomerInfo?>();

        foreach (var customerId in customerIds)
        {
            customers[customerId] = await customerClient.GetByIdAsync(customerId);
        }

        foreach (var response in responses)
        {
            response.Customer = customers[response.CustomerId];
        }

        return responses;
    }

    public async Task<List<InvoiceResponse>> GetByCustomerAsync(Guid customerId)
    {
        var invoices = await repo.GetByCustomerAsync(customerId);

        return invoices.Select(i => ToResponse(i, [])).ToList();
    }

    private static bool IsAllowedStatusChange(InvoiceStatus current, InvoiceStatus next)
    {
        // Utkast till skickad
        if (current == InvoiceStatus.Draft && next == InvoiceStatus.Sent)
        {
            return true;
        }

        // Skickad till betald eller förfallen
        if (current == InvoiceStatus.Sent && (next == InvoiceStatus.Paid || next == InvoiceStatus.Overdue))
        {
            return true;
        }

        // Förfallen till betald
        if (current == InvoiceStatus.Overdue && next == InvoiceStatus.Paid)
        {
            return true;
        }

        return false;
    }

    private static InvoiceStatus CurrentStatus(Invoice invoice)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        if (invoice.Status == InvoiceStatus.Sent && invoice.DueDate < today)
        {
            return InvoiceStatus.Overdue;
        }

        return invoice.Status;
    }

    private static InvoiceResponse ToResponse(Invoice invoice, List<InvoiceLine> lines)
    {
        var status = CurrentStatus(invoice);

        return new InvoiceResponse
        {
            Id = invoice.Id,
            CustomerId = invoice.CustomerId,
            TemplateId = invoice.TemplateId,
            InvoiceNumber = invoice.InvoiceNumber,
            OcrReference = invoice.OcrReference,
            InvoiceDate = invoice.InvoiceDate,
            DueDate = invoice.DueDate,
            AmountExclVat = invoice.AmountExclVat,
            VatAmount = invoice.VatAmount,
            TotalAmount = invoice.TotalAmount,
            Status = status,
            StatusText = InvoiceText.Status(status),

            CanMarkAsSent = IsAllowedStatusChange(invoice.Status, InvoiceStatus.Sent),
            CanMarkAsPaid = IsAllowedStatusChange(invoice.Status, InvoiceStatus.Paid),

            Lines = lines.Select(l => new InvoiceLineResponse
            {
                Id = l.Id,
                LineNumber = l.LineNumber,
                Description = l.Description,
                Quantity = l.Quantity,
                Unit = l.Unit,
                UnitText = InvoiceText.Unit(l.Unit),
                UnitPrice = l.UnitPrice,
                VatRate = l.VatRate,
                Amount = l.Amount
            }).ToList()
        };
    }
}