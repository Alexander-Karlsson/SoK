using System.ComponentModel.DataAnnotations;

namespace Invoices.Application.Dtos;

public class CreateTemplateRequest
{
    [Required(ErrorMessage = "Företagsnamn får inte vara tomt")]
    public string CompanyName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Organisationsnummer måste fyllas i")]
    [RegularExpression(@"^\d{6}-\d{4}$",
        ErrorMessage = "Organisationsnummer ska skrivas 112233-4455")]
    public string CompanyNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Momsnummer måste fyllas i")]
    [RegularExpression(@"^SE\d{12}$",
        ErrorMessage = "Momsnummer ska skrivas som SE556677889901")]
    public string VatNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Adressen måste fyllas i")]
    public string CompanyAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "Postnummer måste fyllas i")]
    [RegularExpression(@"^\d{3} ?\d{2}$",
        ErrorMessage = "Postnummer ska vara 5 siffror, tex 123 45")]
    public string CompanyPostalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Stad/ort måste fyllas i")]
    public string CompanyCity { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefonnummer måste fyllas i")]
    [RegularExpression(@"^(\+46\s?|0)[1-9][\d\s-]{5,13}$",
        ErrorMessage = "Telefonnummer ska skrivas som 070-123 45 67 eller +46701234567")]
    public string CompanyPhone { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-post måste fyllas i")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.(se|com)$",
        ErrorMessage = "E-post ska skrivas som info@foretag.se")]
    public string CompanyEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bankgiro måste fyllas i")]
    [RegularExpression(@"^\d{3,4}-\d{4}$",
       ErrorMessage = "Bankgiro ska skrivas som 123-4567 eller 1234-5678")]
    public string CompanyBankgiro { get; set; } = string.Empty;

    public string Logo { get; set; } = string.Empty;
    public string PrimaryColor { get; set; } = string.Empty; // "000000"
    public string StandardText { get; set; } = string.Empty;

}
