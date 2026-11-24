using Invoices.Application.Dtos;
using Invoices.Application.Interfaces;
using Invoices.Domain.Entities;

namespace Invoices.Application.Services;

public class TemplateService(ITemplateRepository repo)
{
    public async Task<TemplateResponse> CreateTemplateAsync(CreateTemplateRequest request)
    {
        var template = new InvoiceTemplate { Id = Guid.NewGuid() };

        CopyFromRequest(template, request);

        var saved = await repo.AddAsync(template);
        return ToResponse(saved);
    }

    public async Task<TemplateResponse?> UpdateTemplateAsync(Guid id, CreateTemplateRequest request)
    {
        var template = await repo.GetByIdAsync(id);

        if (template == null)
        {
            return null;
        }

        CopyFromRequest(template, request);

        await repo.UpdateAsync(template);
        return ToResponse(template);
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

    private static void CopyFromRequest(InvoiceTemplate template, CreateTemplateRequest request)
    {
        template.CompanyName = request.CompanyName;
        template.CompanyNumber = request.CompanyNumber;
        template.VatNumber = request.VatNumber;
        template.CompanyAddress = request.CompanyAddress;
        template.CompanyPostalCode = request.CompanyPostalCode;
        template.CompanyCity = request.CompanyCity;
        template.CompanyPhone = request.CompanyPhone;
        template.CompanyEmail = request.CompanyEmail;
        template.CompanyBankgiro = request.CompanyBankgiro;
        template.Logo = request.Logo;
        template.PrimaryColor = request.PrimaryColor;
        template.StandardText = request.StandardText;
        template.OurReference = request.OurReference;
    }
}