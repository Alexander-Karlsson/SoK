namespace Invoices.Domain.Entities;

public class InvoiceTemplate
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyNumber { get; set; } = string.Empty;
    public string VatNumber {  get; set; } = string.Empty;
    public string CompanyAddress {  get; set; } = string.Empty;
    public string CompanyPostalCode {  get; set; } = string.Empty;
    public string CompanyCity {  get; set; } = string.Empty;
    public string CompanyPhone {  get; set; } = string.Empty;
    public string CompanyEmail {  get; set; } = string.Empty;
    public string CompanyBankgiro {  get; set; } = string.Empty;
    public string Logo {  get; set; } = string.Empty;
    public string PrimaryColor {  get; set; } = string.Empty;
    public string StandardText {  get; set; } = string.Empty;
}
