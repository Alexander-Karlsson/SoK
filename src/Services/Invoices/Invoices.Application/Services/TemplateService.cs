using Invoices.Application.Dtos;
using Invoices.Application.Interfaces;
using Invoices.Domain.Entities;

namespace Invoices.Application.Services;

public class TemplateService(ITemplateRepository repo)
{
    public async Task<InvoiceTemplate> CreateTemplateAsync(CreateTemplateRequest request)
    {
        var template = new InvoiceTemplate()
        {
            Id = Guid.NewGuid(),
            CompanyName = request.CompanyName,
            CompanyNumber = request.CompanyNumber,
            VatNumber = request.VatNumber,
            CompanyAddress = request.CompanyAddress,
            CompanyPostalCode = request.CompanyPostalCode,
            CompanyCity = request.CompanyCity,
            CompanyPhone = request.CompanyPhone,
            CompanyEmail = request.CompanyEmail,
            CompanyBankgiro = request.CompanyBankgiro,
            Logo = request.Logo,
            PrimaryColor = request.PrimaryColor,
            StandardText = request.StandardText
        };

        return await repo.AddAsync(template);
    }

    public async Task<List<InvoiceTemplate>> GetAllAsync()
        => await repo.GetAllAsync();

    public async Task<InvoiceTemplate?> GetByIdAsync(Guid id)
        => await repo.GetByIdAsync(id);
}
