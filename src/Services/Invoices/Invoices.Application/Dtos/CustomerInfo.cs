namespace Invoices.Application.Dtos;

public class CustomerInfo
{
    public string CustomerNumber { get; set; } = string.Empty;

    // Privat eller företag
    public string CustomerType { get; set; } = string.Empty;

    // Privatperson
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    // Företag
    public string? CompanyName { get; set; }
    public string? OrganisationNumber { get; set; }
    public string? ContactPerson { get; set; }

    // Gemensamma
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
