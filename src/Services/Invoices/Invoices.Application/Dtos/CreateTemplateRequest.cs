namespace Invoices.Application.Dtos;

public class CreateTemplateRequest
{
    public required string CompanyName { get; set; }
    public required string CompanyNumber { get; set; }
    public required string VatNumber { get; set; }
    public required string CompanyAddress { get; set; }
    public required string CompanyPostalCode { get; set; }
    public required string CompanyCity { get; set; }
    public required string CompanyPhone { get; set; }
    public required string CompanyEmail { get; set; }
    public required string CompanyBankgiro { get; set; }

    public string Logo { get; set; } = string.Empty;
    public string PrimaryColor { get; set; } = string.Empty; // "000000"
    public string StandardText { get; set; } = string.Empty;

}
