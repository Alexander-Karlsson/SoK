using Customers.Application.Customers.DTOs;
using Customers.Domain.Entities;
using Customers.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Customers.Application.Customers.Validators;

public static class CustomerValidator
{
    public static void ValidateCreate(CreateCustomerRequest request)
    {
        if (request.CustomerType is null)
            throw new ArgumentException("Kundtyp är obligatorisk.");
        
        ValidateCustomerType(
            request.CustomerType.Value,
            request.FirstName,
            request.LastName,
            request.CompanyName,
            request.OrganisationNumber,
            request.ContactPerson);
        
        ValidateContactInformation(
            request.Address,
            request.PostalCode,
            request.City,
            request.Phone,
            request.Email);
        
        if (string.IsNullOrWhiteSpace(request.CustomerNumber))
            throw new ArgumentException("Kundnummer är obligatoriskt.");

        ValidateMaxLength(request.CustomerNumber, 20, "Kundnummer");
    }
    
    public static void ValidateUpdate(Customer existing, UpdateCustomerRequest request)
    {
        ValidateCustomerType(
            existing.CustomerType,
            request.FirstName,
            request.LastName,
            request.CompanyName,
            request.OrganisationNumber,
            request.ContactPerson);
        
        ValidateContactInformation(
            request.Address,
            request.PostalCode,
            request.City,
            request.Phone,
            request.Email);
    }
    
    private static void ValidateContactInformation(
        string address,
        string postalCode,
        string city,
        string phone,
        string email)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Adress är obligatoriskt.");

        if (string.IsNullOrWhiteSpace(postalCode))
            throw new ArgumentException("Postnummer är obligatoriskt.");

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("Ort är obligatoriskt.");

        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Telefonnummer är obligatoriskt.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("E-postadress är obligatorisk.");

        if (!new EmailAddressAttribute().IsValid(email))
            throw new ArgumentException("Ogiltig e-postadress.");
        
        ValidateMaxLength(address, 100, "Adress");
        ValidateMaxLength(postalCode, 20, "Postnummer");
        ValidateMaxLength(city, 100, "Ort");
        ValidateMaxLength(phone, 30, "Telefonnummer");
        ValidateMaxLength(email, 200, "E-postadress");
    }
    
    private static void ValidateMaxLength(
        string? value,
        int maxLength,
        string fieldName)
    {
        if (value is not null && value.Length > maxLength)
        {
            throw new ArgumentException(
                $"{fieldName} får innehålla högst {maxLength} tecken.");
        }
    }
    
    private static void ValidateCustomerType(CustomerType customerType, string? firstName, string? lastName, string? companyName, string? organisationNumber, string? contactPerson)
    {
        if (customerType == CustomerType.PrivatePerson)
        {
            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException(
                    "Förnamn och efternamn är obligatoriskt.");
            }

            if (companyName is not null ||
                organisationNumber is not null ||
                contactPerson is not null)
            {
                throw new ArgumentException(
                    "Privatkunder kan inte ha företagsinformation.");
            }
        }
        else if (customerType == CustomerType.Company)
        {
            if (string.IsNullOrWhiteSpace(companyName) ||
                string.IsNullOrWhiteSpace(organisationNumber))
            {
                throw new ArgumentException(
                    "Företagsnamn och organisationsnummer är obligatoriskt.");
            }

            if (firstName is not null || lastName is not null)
            {
                throw new ArgumentException(
                    "Företagskunder kan inte ha för-/efternamn.");
            }
        }
        else
        {
            throw new ArgumentException("Ogiltig kundtyp.");
        }
        
        ValidateMaxLength(firstName, 200, "Förnamn");
        ValidateMaxLength(lastName, 100, "Efternamn");
        ValidateMaxLength(companyName, 200, "Företagsnamn");
        ValidateMaxLength(organisationNumber, 20, "Organisationsnummer");
        ValidateMaxLength(contactPerson, 200, "Kontaktperson");
    }
}