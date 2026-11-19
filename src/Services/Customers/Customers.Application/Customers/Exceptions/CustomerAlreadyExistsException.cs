namespace Customers.Application.Customers.Exceptions;

public class CustomerAlreadyExistsException : Exception
{
    public CustomerAlreadyExistsException(string customerNumber) 
        : base($"Kund med {customerNumber} existerar redan.")
    {
    }
}