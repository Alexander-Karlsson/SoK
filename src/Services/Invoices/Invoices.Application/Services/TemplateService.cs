using Invoices.Application.Dtos;
using Invoices.Application.Interfaces;
using Invoices.Domain.Entities;

namespace Invoices.Application.Services;

public class TemplateService(ITemplateRepository repo)
{
    public async Task<TemplateResponse> CreateTemplateAsync(CreateTemplateRequest request) 
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
            StandardText = request.StandardText,
            OurReference = request.OurReference
        };

        var saved = await repo.AddAsync(template);
        return ToResponse(saved);
    }

    public async Task<List<TemplateResponse>> GetAllAsync()
    {
        var templates = await repo.GetAllAsync();
        return templates.Select(ToResponse).ToList();
    }

    public async Task<TemplateResponse?> GetByIdAsync(Guid id)
    {
        var template = await repo.GetByIdAsync(id);
        return template == null ? null : ToResponse(template);
    }

    public static TemplateResponse ToResponse(InvoiceTemplate template)
    {
        return new TemplateResponse
        {
            Id = template.Id,
            CompanyName = template.CompanyName,
            CompanyNumber = template.CompanyNumber,
            VatNumber = template.VatNumber,
            CompanyAddress = template.CompanyAddress,
            CompanyPostalCode = template.CompanyPostalCode,
            CompanyCity = template.CompanyCity,
            CompanyPhone = template.CompanyPhone,
            CompanyEmail = template.CompanyEmail,
            CompanyBankgiro = template.CompanyBankgiro,
            Logo = template.Logo,
            PrimaryColor = template.PrimaryColor,
            StandardText = template.StandardText,
            OurReference = template.OurReference
        };
    }
}