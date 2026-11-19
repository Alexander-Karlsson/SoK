using Customers.Application.Customers.DTOs;
using Customers.Domain.Entities;

namespace Customers.Application.Customers.Mappers;

public static class CustomerMapper
{
    public static CustomerResponseDto ToResponseDto(Customer customer)
    {
        return new CustomerResponseDto
        {
            Id = customer.Id,
            CustomerNumber = customer.CustomerNumber,
            CustomerType = customer.CustomerType,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            CompanyName = customer.CompanyName,
            ContactPerson = customer.ContactPerson,
            OrganisationNumber = customer.OrganisationNumber,
            Address = customer.Address,
            PostalCode = customer.PostalCode,
            City = customer.City,
            Phone = customer.Phone,
            Email = customer.Email,
            IsActive = customer.IsActive
        };
    }
    
    public static Customer ToEntity(CreateCustomerRequest request)
    {
        return new Customer
        {
            CustomerNumber = request.CustomerNumber,
            CustomerType = request.CustomerType ?? throw new ArgumentException("Kundtyp är obligatopriskt."),
            FirstName = request.FirstName,
            LastName = request.LastName,
            CompanyName = request.CompanyName,
            OrganisationNumber = request.OrganisationNumber,
            ContactPerson = request.ContactPerson,
            Address = request.Address,
            PostalCode = request.PostalCode,
            City = request.City,
            Phone = request.Phone,
            Email = request.Email,
            Preferences = request.Preferences
        };
    }
    
    public static void UpdateEntity(Customer customer, UpdateCustomerRequest request)
    {
        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;
        customer.CompanyName = request.CompanyName;
        customer.OrganisationNumber = request.OrganisationNumber;
        customer.ContactPerson = request.ContactPerson;
        customer.Address = request.Address;
        customer.PostalCode = request.PostalCode;
        customer.City = request.City;
        customer.Phone = request.Phone;
        customer.Email = request.Email;
        customer.Preferences = request.Preferences;
    }
}