using Customers.Domain.Enums;

namespace Customers.Application.Customers.DTOs;

public class UpdateCustomerRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CompanyName { get; set; }
    public string? OrganisationNumber { get; set; }
    public string? ContactPerson { get; set; }

    public required string Address { get; set; }
    public required string PostalCode { get; set; }
    public required string City { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }

    public string? Preferences { get; set; }
}